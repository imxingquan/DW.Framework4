using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DW.Framework.Data
{
    /// <summary>
    /// 描述实体映射的数据库表名称
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.Class, Inherited=false,AllowMultiple=false),Serializable]
    public class TableAttribute:Attribute
    {
        
        public TableAttribute()
        {
        }

        public TableAttribute(string tableName)
        {
            this.TableName = tableName;
        }

        /// <summary>
        /// 映射的表名
        /// </summary>
        public string TableName
        {
            set;
            get;
        }
    }


}
