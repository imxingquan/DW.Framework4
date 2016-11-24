/**
 * 大连西数网络技术有限公司 (C) 2007
 * 文件名: DataAccess.cs
 * 创建日期: 2007.5.20
 * 最后修改时间:2011.4.8
 * 描述: 
 *       数据访问通用基类。
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
    /// 访问数据类的抽象类
    /// </summary>
    public abstract class DataAccessor<T> where T:new()
    {

        public string connectionString;

        /// <summary>
        /// 检索特性，返回表名称。如果没有指定[Table("table_name")]使用类名称做表名称。
        /// </summary>
        public string TableName
        {
            get
            {
                //检索特性，如果类有[Table("tablename")]则，使用特性，否则使用类型名称
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
        /// 将对象的null值转换成数据库中的null值
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>返回转换后的对象</returns>
        /// <remarks>有些写入数据库的值可能为null,但是数据库中的null与程序中的null不相同。因此要做转换处理。</remarks>
        public object ConvertDBNull(object obj)
        {
            return obj == null ? DBNull.Value : obj;
        }

        #region SQL语句操作

        /// <summary>
        /// 执行SQL语句，不返回结果。比如：插入、删除、更新操作等。
        /// </summary>
        /// <param name="cmd">DbCommand对象</param>
        /// <returns>返回1表示执行成功</returns>
        protected int ExecuteNonQuery(DbCommand cmd)
        {
            int i = -1;
            try
            {
                foreach (DbParameter param in cmd.Parameters)
                {
                    if (param.Direction == ParameterDirection.Input)
                    {
                        //此处也可以写成
                        //param.Value = ConvertDBNull(param.Value);
                        if (param.Value == null)
                            param.Value = DBNull.Value;
                    }
                }

                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string errmsg = string.Format("执行【{0}】时出错:\r\n{1}\r\n详细错误:{2}", cmd.CommandText, ex.Message, ex.StackTrace);
                DW.Framework.Logger.Log.Write(errmsg);
                
            }
            return i;
        }

        /// <summary>
        /// 执行SQL语句，返回执行结果
        /// </summary>
        /// <param name="cmd">DbCommand对象</param>
        /// <returns>返回SQL语句的结果，结果是一个IDataReader对象</returns>
        protected IDataReader ExecuteReader(DbCommand cmd)
        {
            return ExecuteReader(cmd, CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// 执行SQL语句，返回执行结果
        /// </summary>
        /// <param name="cmd">DbCommand对象</param>
        /// <param name="behavior">CommandBehavior对象</param>
        /// <returns>返回SQL语句的结果，结果是一个IDataReader对象</returns>
        protected IDataReader ExecuteReader(DbCommand cmd, CommandBehavior behavior)
        {
            IDataReader reader = null;
            try
            {
                reader = cmd.ExecuteReader(behavior);

            }
            catch (Exception ex)
            {
                string errmsg = string.Format("执行【{0}】时出错:\r\n{1}\r\n详细错误:{2}", cmd.CommandText, ex.Message, ex.StackTrace);
                if(reader!=null)reader.Close();
                DW.Framework.Utils.Helper.ShowErrorPage(errmsg);
                throw new Exception(errmsg);
            }
            return reader;
        }

        /// <summary>
        /// 执行SQL语句，返回一个值
        /// </summary>
        /// <param name="cmd">DbCommand对象</param>
        /// <returns>返回数据中的一个值</returns>
        /// <remarks>一般用在求数据个数，求和等操作上。</remarks>
        protected object ExecuteScalar(DbCommand cmd)
        {
            object o = null;
            try
            {
                o = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                string errmsg = string.Format("执行【{0}】时出错:\r\n{1}\r\n详细错误:{2}", cmd.CommandText, ex.Message, ex.StackTrace);
                DW.Framework.Logger.Log.Write(errmsg);
            }
            return o;
        }

        #endregion

        protected string UpFirstChar(string strvalue)
        {
            return strvalue.Substring(0, 1).ToUpper() + strvalue.Substring(1, strvalue.Length - 1);
        }

        #region 利用反射给实体赋值

        /// <summary>
        /// 利用反射给实体赋值的泛型,自动返回实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected T ReaderTo(IDataReader reader)
        {
            T targetObj = new T();  //创建类的实例

            IDictionary<string,object>dict = null; //扩展字段对象

            //Field 扩展字段 定义在 EtityBase
            System.Reflection.PropertyInfo propertyExtField = targetObj.GetType().GetProperty("Field");

            for (int i = 0; i < reader.FieldCount; i++)  //遍历数据库字段
            {
                object value = reader.GetValue(i) is DBNull ? null : reader.GetValue(i);
                //查找属性是否使用特性标注来映射表中的字段名称
                if (DataMapper.FillColumnAttribteProperty(targetObj, reader.GetName(i), value))
                    continue;

                System.Reflection.PropertyInfo propertyInfo = targetObj.GetType().GetProperty(UpFirstChar(reader.GetName(i)));
                if (propertyInfo != null) //根据属性名称设置值
                {

                    DataMapper.SetValue(targetObj, propertyInfo, value);
                }
                else if(propertyExtField != null)//设置字段值到字典中
                {
                    //entities 没有这个属性,通过Filed["field_name"]或EntityObj["filed_name"] (this访问器) 来访问
                    //Entity的扩展属性在EntityBase中定义使用字典定义
                    //Field["filed_name"] = value

                    DataMapper.SetValue(ref dict, reader.GetName(i), value);
                }
            }

            //设置扩展字段属性
            if (propertyExtField != null)
            {
                propertyExtField.SetValue(targetObj, dict, null);
            }

            return targetObj;
        }

        /// <summary>
        /// 利用反射给实体赋值
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
        /// 读取IDataReader一条数据
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
                DW.Framework.Logger.Log.Write(string.Format("{0}字段映射时出错:{1}", typeof(T).Name, ex.Message));
                //throw new Exception(string.Format("{0}字段映射时出错:{1}", typeof(T).Name, ex.Message));
            }
            return default(T);
        }

        /// <summary>
        /// 从IDataReader读取所有数据
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>数据集合</returns>
        protected virtual IPagedList<T> GetListFromReader(IDataReader reader)
        {
            IPagedList<T> list = new PagedList<T>();
            while (reader.Read())
            {
                list.Add(GetFromReader(reader));
            }
            reader.Close(); //调用了 close 才可以读出 output 或 returnvalue的值
            return list;
        }
        #endregion

    }

    
}
