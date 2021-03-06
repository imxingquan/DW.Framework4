//-----------------------------------------------------------------------
// <copyright file="Logs.cs" company="im@xingquan.org">
//     Copyright (c) Digitwest.com All rights reserved.
//     Website: http://digitwest.com
//	   Create Date: 2012/7/31 9:04:27
// </copyright>
// <Author>im@xingquan.org</Author>
// <version>1.0</version>
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

namespace DW.Framework.Logger.Entities
{
    public class Logs
    {
        #region Metadata
        
		private int _ID ;
		/// <summary>
		///ID
		/// </summary>
		public int ID 
		{
			get
			{
				return this._ID ;
			}
			set
			{
				this._ID = value ;
			}
		}


		private string _Category ;
		/// <summary>
		///Category
		/// </summary>
		public string Category 
		{
			get
			{
				return this._Category ;
			}
			set
			{
				this._Category = value ;
			}
		}


		private string _LogMsg ;
		/// <summary>
		///LogMsg
		/// </summary>
		public string LogMsg 
		{
			get
			{
				return this._LogMsg ;
			}
			set
			{
				this._LogMsg = value ;
			}
		}


		private DateTime _LogTime ;
		/// <summary>
		///LogTime
		/// </summary>
		public DateTime LogTime 
		{
			get
			{
				return this._LogTime ;
			}
			set
			{
				this._LogTime = value ;
			}
		}




        #endregion Metadata

        #region contructor

		public Logs(){}

        public Logs(int ID,string Category,string LogMsg,DateTime LogTime)
        {
			this._ID = ID;
			this._Category = Category;
			this._LogMsg = LogMsg;
			this._LogTime = LogTime;

        }

	#endregion
    }
}
