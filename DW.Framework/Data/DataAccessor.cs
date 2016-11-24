/**
 * �����������缼�����޹�˾ (C) 2007
 * �ļ���: DataAccess.cs
 * ��������: 2007.5.20
 * ����޸�ʱ��:2011.4.8
 * ����: 
 *       ���ݷ���ͨ�û��ࡣ
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Web;
using System.Web.Caching;
using System.Web.Configuration;

using DW.Framework.Configuration;
using DW.Framework.Pager;


namespace DW.Framework.Data
{

    public enum MaxMin { Max, Min }
    /// <summary>
    /// ����������ĳ�����
    /// </summary>
    public abstract class DataAccessor<T> where T:new()
    {

        public string connectionString;

        /// <summary>
        /// �������ԣ����ر����ơ����û��ָ��[Table("table_name")]ʹ���������������ơ�
        /// </summary>
        public string TableName
        {
            get
            {
                //�������ԣ��������[Table("tablename")]��ʹ�����ԣ�����ʹ����������
                TableAttribute attr = Attribute.GetCustomAttribute(typeof(T), typeof(TableAttribute)) as TableAttribute;
                //TableAttribute[] attr = typeof(T).GetCustomAttributes(typeof(TableAttribute), true) as TableAttribute[];
                if(attr!=null){
                    return attr.TableName;
                }
                else{
                    return typeof(T).Name;
                }
            }

        }

        /// <summary>
        /// �������nullֵת�������ݿ��е�nullֵ
        /// </summary>
        /// <param name="obj">Ҫת���Ķ���</param>
        /// <returns>����ת����Ķ���</returns>
        /// <remarks>��Щд�����ݿ��ֵ����Ϊnull,�������ݿ��е�null������е�null����ͬ�����Ҫ��ת������</remarks>
        public object ConvertDBNull(object obj)
        {
            return obj == null ? DBNull.Value : obj;
        }

        #region SQL������

        /// <summary>
        /// ִ��SQL��䣬�����ؽ�������磺���롢ɾ�������²����ȡ�
        /// </summary>
        /// <param name="cmd">DbCommand����</param>
        /// <returns>����1��ʾִ�гɹ�</returns>
        protected int ExecuteNonQuery(DbCommand cmd)
        {
            int i = -1;
            try
            {
                foreach (DbParameter param in cmd.Parameters)
                {
                    if (param.Direction == ParameterDirection.Input)
                    {
                        //�˴�Ҳ����д��
                        //param.Value = ConvertDBNull(param.Value);
                        if (param.Value == null)
                            param.Value = DBNull.Value;
                    }
                }

                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string errmsg = string.Format("ִ�С�{0}��ʱ����:\r\n{1}\r\n��ϸ����:{2}", cmd.CommandText, ex.Message, ex.StackTrace);
                DW.Framework.Logger.Log.Write(errmsg);
                
            }
            return i;
        }

        /// <summary>
        /// ִ��SQL��䣬����ִ�н��
        /// </summary>
        /// <param name="cmd">DbCommand����</param>
        /// <returns>����SQL���Ľ���������һ��IDataReader����</returns>
        protected IDataReader ExecuteReader(DbCommand cmd)
        {
            return ExecuteReader(cmd, CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// ִ��SQL��䣬����ִ�н��
        /// </summary>
        /// <param name="cmd">DbCommand����</param>
        /// <param name="behavior">CommandBehavior����</param>
        /// <returns>����SQL���Ľ���������һ��IDataReader����</returns>
        protected IDataReader ExecuteReader(DbCommand cmd, CommandBehavior behavior)
        {
            IDataReader reader = null;
            try
            {
                reader = cmd.ExecuteReader(behavior);

            }
            catch (Exception ex)
            {
                string errmsg = string.Format("ִ�С�{0}��ʱ����:\r\n{1}\r\n��ϸ����:{2}", cmd.CommandText, ex.Message, ex.StackTrace);
                if(reader!=null)reader.Close();
                DW.Framework.Utils.Helper.ShowErrorPage(errmsg);
                throw new Exception(errmsg);
            }
            return reader;
        }

        /// <summary>
        /// ִ��SQL��䣬����һ��ֵ
        /// </summary>
        /// <param name="cmd">DbCommand����</param>
        /// <returns>���������е�һ��ֵ</returns>
        /// <remarks>һ�����������ݸ�������͵Ȳ����ϡ�</remarks>
        protected object ExecuteScalar(DbCommand cmd)
        {
            object o = null;
            try
            {
                o = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                string errmsg = string.Format("ִ�С�{0}��ʱ����:\r\n{1}\r\n��ϸ����:{2}", cmd.CommandText, ex.Message, ex.StackTrace);
                DW.Framework.Logger.Log.Write(errmsg);
            }
            return o;
        }

        #endregion

        protected string UpFirstChar(string strvalue)
        {
            return strvalue.Substring(0, 1).ToUpper() + strvalue.Substring(1, strvalue.Length - 1);
        }

        #region ���÷����ʵ�帳ֵ

        /// <summary>
        /// ���÷����ʵ�帳ֵ�ķ���,�Զ�����ʵ�����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected T ReaderTo(IDataReader reader)
        {
            T targetObj = new T();  //�������ʵ��

            IDictionary<string,object>dict = null; //��չ�ֶζ���

            //Field ��չ�ֶ� ������ EtityBase
            System.Reflection.PropertyInfo propertyExtField = targetObj.GetType().GetProperty("Field");

            for (int i = 0; i < reader.FieldCount; i++)  //�������ݿ��ֶ�
            {
                object value = reader.GetValue(i) is DBNull ? null : reader.GetValue(i);
                //���������Ƿ�ʹ�����Ա�ע��ӳ����е��ֶ�����
                if (DataMapper.FillColumnAttribteProperty(targetObj, reader.GetName(i), value))
                    continue;

                System.Reflection.PropertyInfo propertyInfo = targetObj.GetType().GetProperty(UpFirstChar(reader.GetName(i)));
                if (propertyInfo != null) //����������������ֵ
                {

                    DataMapper.SetValue(targetObj, propertyInfo, value);
                }
                else if(propertyExtField != null)//�����ֶ�ֵ���ֵ���
                {
                    //entities û���������,ͨ��Filed["field_name"]��EntityObj["filed_name"] (this������) ������
                    //Entity����չ������EntityBase�ж���ʹ���ֵ䶨��
                    //Field["filed_name"] = value

                    DataMapper.SetValue(ref dict, reader.GetName(i), value);
                }
            }

            //������չ�ֶ�����
            if (propertyExtField != null)
            {
                propertyExtField.SetValue(targetObj, dict, null);
            }

            return targetObj;
        }

        /// <summary>
        /// ���÷����ʵ�帳ֵ
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="targetObj"></param>
        protected void ReaderToObject(IDataReader reader, object targetObj)
        {

            for (int i = 0; i < reader.FieldCount; i++)
            {
                System.Reflection.PropertyInfo propertyInfo = targetObj.GetType().GetProperty(reader.GetName(i));
                if (propertyInfo != null)
                {
                    if (reader.GetValue(i) != DBNull.Value)
                    {

                        if (propertyInfo.PropertyType.IsEnum)
                        {
                            propertyInfo.SetValue(targetObj, Enum.ToObject(propertyInfo.PropertyType, reader.GetValue(i)), null);
                        }
                        else
                        {
                            propertyInfo.SetValue(targetObj, reader.GetValue(i), null);
                        }

                    }
                }
            }


        }

        #endregion

        #region Fill from DataReader

        /// <summary>
        /// ��ȡIDataReaderһ������
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected virtual T GetFromReader(IDataReader reader)
        {
            //T obj = default(T);
            try
            {
                return ReaderTo(reader);
            }
            catch (Exception ex)
            {
                DW.Framework.Logger.Log.Write(string.Format("{0}�ֶ�ӳ��ʱ����:{1}", typeof(T).Name, ex.Message));
                //throw new Exception(string.Format("{0}�ֶ�ӳ��ʱ����:{1}", typeof(T).Name, ex.Message));
            }
            return default(T);
        }

        /// <summary>
        /// ��IDataReader��ȡ��������
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>���ݼ���</returns>
        protected virtual IPagedList<T> GetListFromReader(IDataReader reader)
        {
            IPagedList<T> list = new PagedList<T>();
            while (reader.Read())
            {
                list.Add(GetFromReader(reader));
            }
            reader.Close(); //������ close �ſ��Զ��� output �� returnvalue��ֵ
            return list;
        }
        #endregion

    }

    
}
