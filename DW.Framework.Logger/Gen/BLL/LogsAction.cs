//-----------------------------------------------------------------------
// <copyright file="LogsAction.cs" company="im@xingquan.org">
//     Copyright (c) Digitwest.com All rights reserved.
//     Website: http://digitwest.com
//	   Create Date: 2012/7/31 9:04:27
// </copyright>
// <Author>im@xingquan.org</Author>
// <version>1.0</version>
// <summary>
//   商业逻辑层
//     数据操作使用Provider属性提供，Provider是 DataContainer.GetInstance<T>()的实例
// </summary>
//-----------------------------------------------------------------------

#region using

using System;
using System.Collections.Generic;
using System.Text;
using DW.Framework.Data;
using DW.Framework.Web;
using DW.Framework.Pager;
using DW.Framework.Configuration;
using DW.Framework.Logger.DAL;
using DW.Framework.Logger.Entities;
using DW.Framework.Logger.DAL.Interface;


#endregion

namespace DW.Framework.Logger.BLL
{
    [System.ComponentModel.DataObject]
    public class LogsAction : BizObject<ILogsProvider>
    {
        #region Cache Key
        
		protected static string cacheKey="Logs";

        #endregion
		
		public readonly static LogsAction Call = new LogsAction();

        #region Insert,Update,Delete
		
		/// <summary>
        /// 添加
        /// </summary>
		public int Insert(Logs data)
		{
			return Insert(data.ID,data.Category,data.LogMsg,data.LogTime);
		}
        
		/// <summary>
        /// 添加
        /// </summary>
		[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
		public int Insert(int iD,string category,string logMsg,DateTime logTime)
        { 
            int retVal = Provider.Insert(new Logs(iD,category,logMsg,logTime));
            //clear cache
            PurgeCacheItems(cacheKey);
            return retVal;
        }

		public bool Update(Logs data)
		{
			return Update(data.ID,data.Category,data.LogMsg,data.LogTime);
		}

		/// <summary>
        /// 更新
        /// </summary>
		[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public bool Update(int iD,string category,string logMsg,DateTime logTime)
        {
            bool retVal = Provider.Update(new Logs(iD,category,logMsg,logTime));
            //clear cache
            PurgeCacheItems(cacheKey);
            return retVal;
        }

		/// <summary>
        /// 根据条件更新指定字段内容
        /// </summary>
        /// <param name="where">条件 SQL语句WHERE后面的部分</param>
        /// <param name="modifyField">要更新的字段以及值，可以用多个。</param>
        /// <returns></returns>
        public bool Update(string fields, string where)
        {
            bool retVal = Provider.Update(fields, where);
            PurgeCacheItems(cacheKey);
            return retVal;
        }

		/// <summary>
        /// 删除
        /// </summary>
		[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public bool Delete(int id)
        {
			//clear cache
			PurgeCacheItems(cacheKey);
            return Provider.Delete(id);
        }
		
		
		public bool Delete(string where)
        {
			//clear cache
            PurgeCacheItems(cacheKey);
            return Provider.Delete(where);
        }

        #endregion

       
        #region 取数据基本方法

		/// <summary>
        /// 翻页: 返回数据, 内部已经调用了GetCount方法。
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sortExpression">排序字段</param>
        /// <param name="startRowIndex">起始页</param>
        /// <param name="maximumRows">每页行数 
        /// 可以指定MaxRows.ConfigPageSize常量从配置文件中取PageSize。
        /// MaxRows.AllRows表示取所有行startRowIndex将失效。
        /// </param>
        /// <param name="GetCoutMethod">计算数据数量的方法，默认为空。</param>
        public IPagedList<Logs> GetTables(string where, string sortExpression, int startRowIndex, int maximumRows, CountMethod GetCountMethod)
        {   
			//读取配置文件PageSize
		    if (maximumRows == MaxRows.ConfigPageSize)
                maximumRows = ConfigPageSize;      
            
			//设置缓存参数
			string key = string.Format(cacheKey+"_getall_where{0}_sortExp{1}_startRowIndex{2}_maximumRows{3}", where, sortExpression, startRowIndex, maximumRows);
			
			IPagedList<Logs> tables = Provider.GetTables(where, sortExpression, startRowIndex, maximumRows);

            return CacheData<IPagedList<Logs>>(key,tables).Pager(startRowIndex,maximumRows,where,GetCountMethod);
        }

		/// <summary>
        /// 翻页: 返回数据内部已经调用了GetCount方法
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sortExpression">排序字段</param>
        /// <param name="startRowIndex">起始页</param>
        /// <param name="maximumRows">每页行数 
        /// 可以指定MaxRows.ConfigPageSize常量从配置文件中取PageSize。
        /// MaxRows.AllRows表示取所有行startRowIndex将失效。
        /// </param>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public IPagedList<Logs> GetTables(string where, string sortExpression, int startRowIndex, int maximumRows)
        {
            return GetTables(where, sortExpression, startRowIndex, maximumRows, GetCount);
        }

        /// <summary>
        /// 翻页: 返回数据, 内部已经调用了GetCount方法。
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="startRowIndex">起始页</param>
        /// <param name="maximumRows">每页行数 
        /// 可以指定MaxRows.ConfigPageSize常量从配置文件中取PageSize。
        /// MaxRows.AllRows表示取所有行startRowIndex将失效。
        /// </param>
		[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public IPagedList<Logs> GetTables(string where, int startRowIndex, int maximumRows)
        {
            return GetTables(where, "", startRowIndex, maximumRows);
        }

		/// <summary>
        /// 根据条件获取所有数据,可以排序
        /// </summary>
        public IPagedList<Logs> GetTables(string where, string sortExpression)
        {
            return GetTables(where, sortExpression, 0, MaxRows.AllRows, null);
        }

        /// <summary>
        /// 根据条件获取所有数据
        /// </summary>
        /// <param name="where">条件表达式</param>
		[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public IPagedList<Logs> GetTables(string where)
        {
            return GetTables(where, "", 0, MaxRows.AllRows, null);
        }

        /// <summary>
        /// 返回所有数据
        /// </summary>
        public IPagedList<Logs> GetTables()
        {
            return GetTables("");
        }
		
		/// <summary>
        /// 根据条件计算数据个数
        /// </summary>
        public int GetCount(string where)
        {
            string key = cacheKey+"_getcount_" + where ;   
            
			int count = Provider.GetCount(where);
            
            return CacheData<int>(key,count);
        }

		/// <summary>
        /// 根据主键ID返回数据
        /// </summary>
        public Logs GetById(int id)
        {
            return GetByWhere("ID="+id);
        }

		/// <summary>
        /// 根据条件返回单条数据
        /// </summary>
		public Logs GetByWhere(string where)
        {
            string key = cacheKey+"_bywhere_" + where;
            
			Logs row = Provider.GetByWhere(where);

            return CacheData<Logs>(key,row);
        }
        #endregion

        
    }
}
