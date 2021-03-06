﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using DW.Framework;
using DW.Framework.Logger;

namespace DW.Framework.Data
{
    public class SqlHelper
    {


        /// <summary>
        /// 生成翻页SQL语句,必须要排序字段
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="findExp">查询条件，不需加 WHERE 关键字</param>
        /// <param name="sortExpression">排序字段，不需要加 ORDER 关键字</param>
        /// <param name="startRowIndex">当前页</param>
        /// <param name="maximumRows">每页数量</param>
        /// <returns></returns>
        public static string GetQuerySql(string tableName, string findExp, string sortExpression, int startRowIndex, int maximumRows)
        {
            Debug.Assert(!string.IsNullOrEmpty(tableName));
            //Debug.Assert( maximumRows > 0, "maximumRows必须大于0的数.");

            if (startRowIndex <= 0) startRowIndex = 1;
            /*
            string orderby = "SELECT * FROM ({0}) AS {2} ORDER BY {1} ";         

            string sql = "SELECT TOP {0} * FROM (SELECT TOP ({1}*{0}) * FROM {2} {3} ORDER BY <sort_key1> ) AS {2} ORDER BY <sort_key2>";
            //string sql = "SELECT TOP {0} * FROM (SELECT TOP (({1}/{0}+1)*{0}) * FROM {2} {3} ORDER BY ID ASC ) AS {2} ORDER BY ID DESC";

            if (!string.IsNullOrEmpty(findExp))
                sql = string.Format(sql, maximumRows, startRowIndex, tableName, " WHERE " + findExp);
            else
                sql = string.Format(sql, maximumRows, startRowIndex, tableName, "");
            
            if (!string.IsNullOrEmpty(sortExpression)) //自己指定排序
            {
                string before_sort = sortExpression;
                //调整分页排序
                sortExpression = sortExpression.ToUpper();
                sql = sql.Replace("<sort_key1>", sortExpression);
                
                if (sortExpression.Contains("DESC"))
                {
                    sortExpression = sortExpression.Replace("DESC", "ASC");
                }
                else if (sortExpression.Contains("ASC"))
                {
                    sortExpression = sortExpression.Replace("ASC", "DESC");
                }
                else
                {
                    sortExpression = sortExpression + " DESC";
                }
                sql = sql.Replace("<sort_key2>", sortExpression);
                //总排序
                sql = string.Format(orderby, sql, before_sort, tableName);
            }
            else //默认为ID字段排序,
            {
                sql = sql.Replace("<sort_key1>", "ID ASC");
                sql = sql.Replace("<sort_key2>", "ID DESC");
            }
             */
            //新翻页方法
            if (string.IsNullOrEmpty(sortExpression)) //默认排序
                sortExpression = "ID ASC";


            string sql = "SELECT TOP {0} * FROM [{1}] WHERE [{1}].ID NOT IN(SELECT TOP {2} [{1}].ID FROM [{1}] {4} ORDER BY {3}) {5} ORDER BY {3}";
            int pages = maximumRows * startRowIndex - maximumRows;
            if (!string.IsNullOrEmpty(findExp))
                sql = string.Format(sql, maximumRows, tableName, pages, sortExpression, "WHERE " + findExp, "AND (" + findExp + ")");
            else
                sql = string.Format(sql, maximumRows, tableName, pages, sortExpression, "", "");

            DW.Framework.Logger.Log.Trace(sql);
            return sql;
        }



        /// <summary>
        /// 生成SQL 语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="findExp">查询条件，不需加 WHERE 关键字</param>
        /// <param name="sortExpression">排序字段，不需要加 ORDER 关键字</param>
        /// <returns></returns>
        public static string GetQuerySql(string tableName, string findExp, string sortExpression)
        {
            string sql = string.Format("SELECT * FROM [{0}]", tableName);

            if (!string.IsNullOrEmpty(findExp))
                sql += " WHERE " + findExp;
            if (!string.IsNullOrEmpty(sortExpression))
                sql += " ORDER BY " + sortExpression;

            Log.Trace(sql);
            return sql;
        }

        /// <summary>
        /// maximumRows是零返回所有数据,否则返回分页sql语句,
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="findExp"></param>
        /// <param name="sortExpression"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static string GetSql(string tableName, string findExp, string sortExpression, int startRowIndex, int maximumRows)
        {

            string sql = "";
            if (maximumRows == 0)
                sql = SqlHelper.GetQuerySql(tableName, findExp, sortExpression);
            else
            {

                sql = SqlHelper.GetQuerySql(tableName, findExp, sortExpression, startRowIndex, maximumRows);
            }
            //sql = SqlHelper.GetQuerySql(tableName, findExp, sortExpression, startRowIndex, maximumRows);
            return sql;
        }

        /// <summary>
        /// 根据条件生成求数量的sql语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="findExp"></param>
        /// <returns></returns>
        public static string GetCountSql(string tableName, string findExp)
        {
            string sql = string.Format("SELECT COUNT(*) FROM [{0}]",tableName);

            if (!string.IsNullOrEmpty(findExp))
                sql += " WHERE " + findExp;

            Log.Trace(sql);

            return sql;
        }

        /// <summary>
        /// 生成根据条件取一条的 sql 语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static string GetBySql(string tableName, string where)
        {
            string sql = "SELECT TOP 1 * FROM [{0}]";

            if (!string.IsNullOrEmpty(where))
                sql = sql + " WHERE " + where;

            sql = string.Format(sql, tableName);

            Log.Trace(sql);

            return sql;
        }

        /// <summary>
        /// 生成delete SQL语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static string GetDelSql(string tableName, string where)
        {
            string sql = "DELETE FROM [{0}]";
            if (!string.IsNullOrEmpty(where))
                sql = sql + " WHERE " + where;

            sql = string.Format(sql, tableName);

            Log.Trace(sql);

            return sql;
        }

        #region 关联表操作

        /// <summary>
        /// 生成取关联表数据sql语句
        /// </summary>
        /// <param name="table1">主表</param>
        /// <param name="table2">从表</param>
        /// <param name="table1JoinKey">主表关联key，例如："A.CID"</param>
        /// <param name="table2JoinKey">主表关联key,例如："B.ID"</param>
        /// <param name="table2Field">从表字段,用逗号隔开，例如:"B.field1,B.field2"</param>
        /// <param name="whatjoin">关联方式,"LEFT JOIN"、"RIGHT JOIN"、"INNER JOIN"</param>
        /// <param name="findExp">查询条件</param>
        /// <param name="sortExpression">排序字段</param>
        /// <param name="startRowIndex">当前页</param>
        /// <param name="maximumRows">每页行数</param>
        /// <returns></returns>
        public static string GetQuerySql(string table1, string table2, string table1JoinKey, string table2JoinKey, string table2Field, string whatjoin, string findExp, string sortExpression, int startRowIndex, int maximumRows)
        {
            //Debug.Assert(!string.IsNullOrEmpty(tableName));
            //Debug.Assert( maximumRows > 0, "maximumRows必须大于0的数.");

            if (startRowIndex <= 0) startRowIndex = 1;
            /*
            string orderby = "SELECT * FROM ({0}) AS {2} ORDER BY {1} ";

            //string sql = "SELECT TOP {0} * FROM (SELECT TOP ({1}*{0}) {2}.*,{3AdminGroup.Permission} FROM {2} LEFT JOIN {4} ON {5Admin.GroupID} = {6AdminGroup.ID} {7} ORDER BY {2}.ID ASC ) AS {2} ORDER BY ID DESC";
            // string sql = "SELECT TOP {0} * FROM (SELECT TOP ({1}*{0}) {2}.*,{3} FROM {2} LEFT JOIN {4} ON {5} = {6} {7} ORDER BY {2}.ID ASC ) AS {2} ORDER BY ID DESC";
            string sql = "SELECT TOP {0} * FROM (SELECT TOP ({1}*{0}) {2}.*,{3} FROM {2} {4} {5} ON {6} = {7} {8} ORDER BY {2}.<sort_key1> ) AS {2} ORDER BY <sort_key2>";

            //string sql = "SELECT TOP {0} * FROM (SELECT TOP (({1}/{0}+1)*{0}) * FROM {2} {3} ORDER BY ID ASC ) AS {2} ORDER BY ID DESC";

            if (!string.IsNullOrEmpty(findExp))
                findExp = " WHERE " + findExp;

            sql = string.Format(sql, maximumRows, startRowIndex, table1, table2Field, whatjoin, table2, table1JoinKey, table2JoinKey, findExp);


            if (!string.IsNullOrEmpty(sortExpression)) //自己指定排序
            {
                string before_sort = sortExpression; //记住总排序

                //调整分页排序
                sortExpression = sortExpression.ToUpper();
                sql = sql.Replace("<sort_key1>", sortExpression);

                if (sortExpression.Contains("DESC")) //如果是降序 换成升序
                {
                    sortExpression = sortExpression.Replace("DESC", "ASC");
                }
                else if (sortExpression.Contains("ASC")) //如果是升序换成降序
                {
                    sortExpression = sortExpression.Replace("ASC", "DESC");
                }
                else //默认是升序
                {
                    sortExpression = sortExpression + " DESC";
                }
                sql = sql.Replace("<sort_key2>", sortExpression);

                //总排序
                sql = string.Format(orderby, sql, before_sort, table1);
            }
            else //默认为ID字段排序,如果数据库中不存在ID字段会出错，有待改进
            {
                sql = sql.Replace("<sort_key1>", "ID ASC");
                sql = sql.Replace("<sort_key2>", "ID DESC");
            }*/

            //新翻页方法
            if (string.IsNullOrEmpty(sortExpression)) //默认排序
                sortExpression = "ID ASC";


            string sql = "SELECT TOP {0} {1}.*,{2} FROM [{1}] {3} [{4}] ON [{1}].{5} = [{4}].{6} WHERE [{1}].ID NOT IN(SELECT TOP {7} [{1}].ID FROM [{1}] {3} [{4}] ON [{1}].{5} = [{4}].{6} {9} ORDER BY {8}) {10} ORDER BY {8}";

            int pages = maximumRows * startRowIndex - maximumRows;
            if (!string.IsNullOrEmpty(findExp))
                sql = string.Format(sql, maximumRows, table1, table2Field, whatjoin, table2, table1JoinKey, table2JoinKey, pages, sortExpression, "WHERE " + findExp, "AND (" + findExp + ")");
            else
                sql = string.Format(sql, maximumRows, table1, table2Field, whatjoin, table2, table1JoinKey, table2JoinKey, pages, sortExpression, "", "");

            Log.Trace(sql);
            return sql;
        }

        /// <summary>
        /// 生成取关联表数量sql语句
        /// </summary>
        /// <param name="table1">主表</param>
        /// <param name="table2">从表</param>
        /// <param name="table1JoinKey">主表关联key</param>
        /// <param name="table2JoinKey">主表关联key</param>
        /// <param name="whatjoin">关联方式,"LEFT JOIN"、"RIGHT JOIN"、"INNER JOIN"</param>
        /// <param name="findExp">查询条件</param>
        /// <returns></returns>
        public static string GetCountSql(string table1, string table2, string table1JoinKey, string table2JoinKey, string whatjoin, string findExp)
        {
            //string sql="SELECT count(*) FROM Admin left join AdminGroup ON Admin.GroupID = AdminGroup.ID  "
            string sql = "SELECT count(*) FROM [{0}] {1} [{2}] ON [{0}].{3}= [{2}].{4}";

            sql = string.Format(sql, table1, whatjoin, table2, table1JoinKey, table2JoinKey);
            if (!string.IsNullOrEmpty(findExp))
                sql += " WHERE " + findExp;

            Log.Trace(sql);

            return sql;
        }

        /// <summary>
        /// 生成sum SQL
        /// </summary>
        /// <param name="p"></param>
        /// <param name="findExp"></param>
        /// <returns></returns>
        public static string GetSumSql(string table, string sumField, string findExp)
        {
            string sql = string.Format("SELECT SUM({0}) FROM [{1}]", sumField, table);
            if (!string.IsNullOrEmpty(findExp))
                sql += " WHERE " + findExp;

            Log.Trace(sql);

            return sql;
        }

        #endregion


        //        public static void TraceWrite(string sql)
        //        {


        //             Debug.WriteLine(sql);

        //#if WEB_TRACE

        //            if(System.Web.HttpContext.Current.Trace.IsEnabled)
        //                System.Web.HttpContext.Current.Trace.Warn("SQL",sql);
        //#endif

        //        }

        private static string EncodeExp(string findExp)
        {
            return findExp.Replace("'", "");
        }


    }
}
