using System.Diagnostics;
using DW.Framework.Logger;

namespace DW.Framework.Data
{
    public class SQLiteHelper
    {


        /// <summary>
        /// 生成翻页SQL语句
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
            if (string.IsNullOrEmpty(sortExpression)) //默认排序
                sortExpression = "ID ASC";

            string sql = "SELECT * FROM {0} {1} ORDER BY {2} limit {3},{4}";
            int pages = maximumRows * startRowIndex - maximumRows;
            if (!string.IsNullOrEmpty(findExp))
                sql = string.Format(sql, tableName, "WHERE " + findExp, sortExpression, pages, maximumRows);
            else
                sql = string.Format(sql, tableName, "", sortExpression, pages, maximumRows);

            Log.Trace(sql);
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
            if (string.IsNullOrEmpty(sortExpression)) //默认排序
                sortExpression = "ID ASC";

            string sql = "SELECT  * FROM " + tableName;

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
                sql = GetQuerySql(tableName, findExp, sortExpression);
            else
            {
                sql = GetQuerySql(tableName, findExp, sortExpression, startRowIndex, maximumRows);
            }
            Log.Trace(sql);
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
            string sql = "SELECT COUNT(*) FROM " + tableName;

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
            string sql = "SELECT * FROM {0}";

            if (!string.IsNullOrEmpty(where))
                sql = sql + " WHERE " + where;
            sql += " limit 0,1";

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
            string sql = "DELETE FROM {0}";
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
            if (startRowIndex <= 0) startRowIndex = 1;

            if (string.IsNullOrEmpty(sortExpression)) //默认排序
                sortExpression = "ID ASC";

            string sql = "SELECT {0}.*,{1} FROM {0} {2} {3} ON {4} = {5} {6} ORDER BY {7} limit {8},{9}";

            int pages = maximumRows * startRowIndex - maximumRows;
            if (!string.IsNullOrEmpty(findExp))
                sql = string.Format(sql, table1, table2Field, whatjoin, table2, table1JoinKey, table2JoinKey, "WHERE " + findExp, sortExpression, pages, maximumRows);
            else
                sql = string.Format(sql, table1, table2Field, whatjoin, table2, table1JoinKey, table2JoinKey, "", sortExpression, pages, maximumRows);

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
            string sql = "SELECT count(*) FROM {0} {1} {2} ON {3}= {4}";

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
            string sql = string.Format("SELECT SUM({0}) FROM {1}", sumField, table);
            if (!string.IsNullOrEmpty(findExp))
                sql += " WHERE " + findExp;

            Log.Trace(sql);

            return sql;
        }

        #endregion


        private static string EncodeExp(string findExp)
        {
            return findExp.Replace("'", "");
        }


    }
}
