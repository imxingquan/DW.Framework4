/*************************************************
 * 2011.6.8
 * im@xingquan.org
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DW.Framework.Data
{
    /// <summary>
    /// 描述实体字段的数据库特性
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.Property, Inherited = false, AllowMultiple = false), Serializable]
    public class ColumnAttribute : Attribute
    {
       
        public ColumnAttribute()
        {
        }

        public ColumnAttribute(string columName)
        {
            this.ColumName = columName;
        }
                
        /// <summary>
        /// 映射数据库的列名称
        /// </summary>
        public virtual string ColumName
        {
            set;
            get;
        }
                
    }

}
