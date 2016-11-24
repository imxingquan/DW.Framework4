//-----------------------------------------------------------------------
// <copyright file="$file$" company="$company$">
//     Copyright (c) Digitwest.com All rights reserved.
//     Website: http://digitwest.com
//	   Create Date: $date$
// </copyright>
// <Author>$author$</Author>
// <version>$ver$</version>
// <summary>
//  这个类是数据操作接口的具体实现，接口定义在IXXProvider中定义，
//  使用 DataAccessor访问器 对数据库进行操作
// </summary>
//-----------------------------------------------------------------------

#region using

using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DW.Framework.Data;
using DW.Framework.Pager;
using $namespace$.DAL.Interface;
using $namespace$.Entities;

#endregion

namespace $namespace$.DAL.SqlClient
{
    public class Sql$class$Provider : DataAccessor<$class$>,I$class$Provider
    {

		#region 连接字符串 
        public string ConnectionString
        {
            get
            {
                return this.connectionString;
            }
            set
            {
                this.connectionString = value;
            }
        }
        #endregion

        public int Insert($class$ table)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
				string sql = string.Format("INSERT INTO [{0}] $insert_sql$",TableName);
                SqlCommand cmd = new SqlCommand(sql, cn);
$insert_param$
                //cmd.Parameters.Add("@ID ", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                int retVal = ExecuteNonQuery(cmd);
                return (int)retVal;
            }
        }

        public bool Update($class$ table)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
				string sql = string.Format("UPDATE [{0}] SET $update_sql$",TableName);
                SqlCommand cmd = new SqlCommand(sql, cn);
$update_param$
                //cmd.Parameters.Add("@ID", SqlDbType.Int).Value = table.ID;
                cn.Open();
                int retVal = ExecuteNonQuery(cmd);
                return (retVal == 1);
            }
        }
		
		public bool Update(string fields, string where)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sql ="UPDATE [{0}] SET {1} WHERE {2}";
                sql = string.Format(sql,TableName, fields, where);
                SqlCommand cmd = new SqlCommand(sql, cn);
                cn.Open();
                int retVal = ExecuteNonQuery(cmd);
                return retVal == 1;
            }
        }
		
        public bool Delete(int id)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sql = SqlHelper.GetDelSql(TableName, "ID="+id);
                SqlCommand cmd = new SqlCommand(sql, cn);
                cn.Open();
                int retVal = ExecuteNonQuery(cmd);
                return retVal == 1;
            }
        }
		
		
		public bool Delete(string where)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sql = SqlHelper.GetDelSql(TableName, where);
                SqlCommand cmd = new SqlCommand(sql, cn);
                cn.Open();
                int retVal = ExecuteNonQuery(cmd);
                return retVal == 1;
            }
        }

        public IPagedList<$class$> GetTables(string where, string sortExpression, int startRowIndex, int maximumRows)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sql = SqlHelper.GetSql(TableName, where, sortExpression, startRowIndex, maximumRows);
                SqlCommand cmd = new SqlCommand(sql, cn);
                cn.Open();
                return GetListFromReader(ExecuteReader(cmd));
            }
        }

        public int GetCount(string where)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sql = SqlHelper.GetCountSql(TableName, where);

                SqlCommand cmd = new SqlCommand(sql, cn);
                
                cn.Open();

                return (int)ExecuteScalar(cmd);
            }
        }
        
		public $class$ GetByWhere(string where)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
               string sql = SqlHelper.GetBySql(TableName, where);

                SqlCommand cmd = new SqlCommand(sql, cn);
                
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return GetFromReader(reader);
                return null;
            }
        }

		public $class$ GetById(int id)
        {
            return GetByWhere("ID="+id);
        }
 		public int GetMaxID()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sql = string.Format("SELECT MAX(ID) FROM {0}", TableName);

                SqlCommand cmd = new SqlCommand(sql, cn);

                cn.Open();

                return (int)ExecuteScalar(cmd);
            }
        }

    }
}
