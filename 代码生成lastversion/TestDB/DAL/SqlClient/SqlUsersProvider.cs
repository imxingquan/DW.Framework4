//-----------------------------------------------------------------------
// <copyright file="SqlUsersProvider.cs" company="$company$">
//     Copyright (c) Digitwest.com All rights reserved.
//     Website: http://digitwest.com
//	   Create Date: 2012/5/31 3:06:34
// </copyright>
// <Author>im@xingquan.org</Author>
// <version>1.0.1</version>
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
using TestDB.DAL.Interface;
using TestDB.Entities;

#endregion

namespace TestDB.DAL.SqlClient
{
    public class SqlUsersProvider : DataAccessor<Users>,IUsersProvider
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

        public int Insert(Users table)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
				string sql = string.Format("INSERT INTO [{0}] ([UserName],[Pass],[FullName],[Mobile]) VALUES (@UserName,@Pass,@FullName,@Mobile)",TableName);
                SqlCommand cmd = new SqlCommand(sql, cn);
				cmd.Parameters.Add("@UserName",SqlDbType.NVarChar ).Value = table.UserName;
				cmd.Parameters.Add("@Pass",SqlDbType.NVarChar ).Value = table.Pass;
				cmd.Parameters.Add("@FullName",SqlDbType.NVarChar ).Value = table.FullName;
				cmd.Parameters.Add("@Mobile",SqlDbType.NVarChar ).Value = table.Mobile;

                //cmd.Parameters.Add("@ID ", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                int retVal = ExecuteNonQuery(cmd);
                return (int)retVal;
            }
        }

        public bool Update(Users table)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
				string sql = string.Format("UPDATE [{0}] SET [UserName] = @UserName,[Pass] = @Pass,[FullName] = @FullName,[Mobile] = @Mobile WHERE ID=@ID",TableName);
                SqlCommand cmd = new SqlCommand(sql, cn);
				cmd.Parameters.Add("@ID",SqlDbType.Int ).Value = table.ID;
				cmd.Parameters.Add("@UserName",SqlDbType.NVarChar ).Value = table.UserName;
				cmd.Parameters.Add("@Pass",SqlDbType.NVarChar ).Value = table.Pass;
				cmd.Parameters.Add("@FullName",SqlDbType.NVarChar ).Value = table.FullName;
				cmd.Parameters.Add("@Mobile",SqlDbType.NVarChar ).Value = table.Mobile;

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

        public IPagedList<Users> GetTables(string where, string sortExpression, int startRowIndex, int maximumRows)
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
        
		public Users GetByWhere(string where)
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

		public Users GetById(int id)
        {
            return GetByWhere("ID="+id);
        }
    }
}
