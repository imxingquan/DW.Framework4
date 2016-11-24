using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace DW.Framework.Data.Backup
{
    public class SqlHelper
    {
        public static   string ConnectionString { get; set; }

        /// <summary>
        /// 执行sql 返回 DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sql)
        {   
            
            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cn.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, ConnectionString);

                    DataSet ds = new DataSet();
                    sqlAdapter.Fill(ds);
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                string errmsg = string.Format("执行【{0}】时出错:\r\n{1}\r\n详细错误:{2}", sql, ex.Message, ex.StackTrace);
                DW.Framework.Utils.Helper.ShowErrorPage(errmsg);
                return null;
            }
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>成功返回大于0</returns>
        public static int ExecuteNonQuery(string sql)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string errmsg = string.Format("执行【{0}】时出错:\r\n{1}\r\n详细错误:{2}", sql, ex.Message, ex.StackTrace);
                DW.Framework.Utils.Helper.ShowErrorPage(errmsg);
                return -1;
            }
        }
    }
}
