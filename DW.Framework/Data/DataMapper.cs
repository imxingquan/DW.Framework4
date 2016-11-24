/*************************************************
 * 2011.6.8
 * im@xingquan.org
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;

namespace DW.Framework.Data
{
    public class DataMapper
    {
        /// <summary>
        /// 填充由Column特性标识的属性，如果填充成功返回true
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool FillColumnAttribteProperty(object target, string filedname, object value)
        {
            foreach (PropertyInfo item in target.GetType().GetProperties())
            {
                ColumnAttribute attr = Attribute.GetCustomAttribute(item, typeof(ColumnAttribute)) as ColumnAttribute;
                if (attr !=null && (attr.ColumName.ToLower() == filedname.ToLower()))
                {
                    DataMapper.SetValue(target, item, value);
                    return true;
                }
            }
            return false;
        }


        public static void SetValue(ref IDictionary<string, object> dict, string fieldname, object value)
        {

            if (dict == null) //new dictionary
                dict = new Dictionary<string, object>();

            if(!dict.ContainsKey(fieldname))
                dict.Add(fieldname.ToLower(), value);

        }

        /// <summary>
        /// 填充属性值
        /// </summary>
        /// <param name="target"></param>
        /// <param name="propertyInfo"></param>
        /// <param name="value"></param>
        public static void SetValue(object target, PropertyInfo propertyInfo, object value)
        {
            try
            {
                //PropertyInfo propertyInfo = target.GetType().GetProperty(propertyName);
                if (value == null)
                    propertyInfo.SetValue(target, value, null);
                else
                {
                    Type pType = GetPropertyType(propertyInfo.PropertyType);
                    if (pType.Equals(value.GetType()))
                    {
                        //类型相符，复制数据
                        propertyInfo.SetValue(target, value, null);
                    }
                    else
                    {
                        //类型不相符，强制转换
                        if (pType.Equals(typeof(Guid))) //guid
                            propertyInfo.SetValue(target, new Guid(value.ToString()), null);
                        else //other
                            propertyInfo.SetValue(target, Convert.ChangeType(value, pType), null);
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("填充属性出错property:{0} value:{1},error:{2}", target.ToString(), value, ex.Message));
            }
        }


        /// <summary>
        /// 获取属性的类型，可以处理Nullable<T>类型值得属性的真正类型
        /// </summary>
        /// <param name="propertyType"></param>
        /// <returns></returns>
        public static Type GetPropertyType(Type propertyType)
        {
            Type type = propertyType;
            if (type.IsGenericType &&
                (type.GetGenericTypeDefinition() == typeof(Nullable)))
                return type.GetGenericArguments()[0];
            return type;
        }
    }
}
