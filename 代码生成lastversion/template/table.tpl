//-----------------------------------------------------------------------
// <copyright file="$file$" company="$company$">
//     Copyright (c) Digitwest.com All rights reserved.
//     Website: http://digitwest.com
//	   Create Date: $date$
// </copyright>
// <Author>$author$</Author>
// <version>$ver$</version>
// <summary>
// 实体类可以启用TableAttribute和ColumnAttribute特性
// TableAttribute特性：自定义实体类对应的表名称，主要处理当数据库的表名称发生变化后，不需要修改sql里的表名称。
// ColumnAttribute特性：自定义实体类属性对应的字段名称。主要有两种用途：
//                1. 当数据库中的字段改名后，可以使用ColumnAttribute指定属性映射的数据库字段名称
//                2. 扩展实体的时候可以使用，例如关联表夺取的字段可以使用此特性做映射
// 例如:  
//      [Table("table_name")]
//      public class MyEntites{} 
//      数据映射将根据TableAttribute指定的名称作为实体对应的表名称
//		
//		[Column("product_price")]  映射数据库字段
//      public double Price
//      { get; set;}
// 
// 实体类还可以使用 EntityBase做为基类来扩展实体类。例如：
//      public class MyEntities: EntityBase{}
//  访问扩展字段的方法是:  obj.Field["price"] 或 obj["price"]
// </summary>
//-----------------------------------------------------------------------

using System;

namespace $namespace$.Entities
{
    public partial class $class$
    {
        #region Metadata
        
$metadata$

        #endregion Metadata

        #region contructor

		public $class$(){}

        public $class$($param$)
        {
$initparam$
        }

	#endregion
    }
}
