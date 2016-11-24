using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DW.Framework.Configuration;
using System.Data;
using DW.Framework.Zip;
using System.Web;
using System.IO;
using System.Configuration;


namespace DW.Framework.Data.Backup
{
    public class DataBackup
    {
        public string filename = string.Empty;
        public int iCount = 0;

        public string RoundNum = string.Empty;
        public string strfilenamearr;
        public string strtmpname;
        public StringBuilder templateBuilder;
        public string tmpfilename = string.Empty;
        public string tmpnextfilename = string.Empty;

       
        public string bakupPath;
        public string downPath;

        public Dictionary<string, string> GetZipFileList()
        {
            return null;
        }
         

        public DataBackup(string downPath)
        {
            SqlHelper.ConnectionString = ConfigurationManager.ConnectionStrings["DbBackupServices"].ToString();
            this.filename = "backup" + DateTime.Now.ToString("yyMMdd") + "_{RoundNum}_{i}.txt";
            this.downPath = downPath;
            this.bakupPath = CreateRndDir();
        }

        private string CreateRndDir()
        {
            string path = this.downPath +"/temp_" + DateTime.Now.ToString("yyMMdd") + "/";
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));
            if (Directory.Exists(HttpContext.Current.Server.MapPath(path)))
                return path;
            return "temp";
        }
      
        public void backup()
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            StringBuilder builder3 = new StringBuilder();
            StringBuilder builder4 = new StringBuilder();
            StringBuilder builder5 = new StringBuilder();
            StringBuilder builder6 = new StringBuilder();
            bool flag = false;
            DataTable allTable = null;
            DataTable fieldByTableName = null;
            DataTable allDataByTableName = null;
            int num = 0;
            int num2 = 0;
            int count = 0;
            allTable = this.GetAllTable();
            foreach (DataRow row in allTable.Rows)
            {
                flag = false;
                builder2.Remove(0, builder2.Length);
                builder3.Remove(0, builder3.Length);
                builder4.Remove(0, builder4.Length);
                builder5.Remove(0, builder5.Length);
                builder.Append("IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[" + row["name"].ToString() + "]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) DROP TABLE [dbo].[" + row["name"].ToString() + "] \r \n");
                fieldByTableName = this.GetFieldByTableName(row["name"].ToString());
                count = fieldByTableName.Rows.Count;
                num = 0;
                builder.Append("CREATE TABLE dbo." + row["name"].ToString() + "(");
                builder2.Append("CREATE TABLE dbo.Tmp_" + row["name"].ToString() + "(");
                foreach (DataRow row2 in fieldByTableName.Rows)
                {
                    if ((row2["type"].ToString() == "nvarchar") || (row2["type"].ToString() == "nchar"))
                    {
                        if (int.Parse(row2["length"].ToString()) == -1)
                        {
                            builder.Append(string.Concat(new object[] { "[", row2["name"], "] [", row2["type"], "] (MAX) " }));
                            builder2.Append(string.Concat(new object[] { "[", row2["name"], "] [", row2["type"], "] (MAX) " }));
                        }
                        else
                        {
                            builder.Append(string.Concat(new object[] { "[", row2["name"], "] [", row2["type"], "] (", int.Parse(row2["length"].ToString()) / 2, ") " }));
                            builder2.Append(string.Concat(new object[] { "[", row2["name"], "] [", row2["type"], "] (", int.Parse(row2["length"].ToString()) / 2, ") " }));
                        }
                    }
                    else if ((row2["type"].ToString() == "varchar") || (row2["type"].ToString() == "char"))
                    {
                        builder.Append(string.Concat(new object[] { "[", row2["name"], "] [", row2["type"], "] (", row2["length"], ")" }));
                        builder2.Append(string.Concat(new object[] { "[", row2["name"], "] [", row2["type"], "] (", row2["length"], ")" }));
                    }
                    else
                    {
                        builder.Append(string.Concat(new object[] { "[", row2["name"], "] [", row2["type"], "] " }));
                        builder2.Append(string.Concat(new object[] { "[", row2["name"], "] [", row2["type"], "] " }));
                    }
                    builder3.Append("[" + row2["name"] + "]");
                    if (row2["type"].ToString() == "decimal")
                    {
                        builder.Append(" (" + row2["precision"].ToString() + "," + row2["scale"].ToString() + ")");
                        builder2.Append(" (" + row2["precision"].ToString() + "," + row2["scale"].ToString() + ")");
                    }
                    if (Convert.ToBoolean(row2["is_nullable"]))
                    {
                        builder.Append(" NULL");
                        builder2.Append(" NULL");
                    }
                    else
                    {
                        builder.Append(" NOT NULL");
                        builder2.Append(" NOT NULL");
                    }
                    if (Convert.ToBoolean(row2["is_identity"]))
                    {
                        flag = true;
                        builder2.Append(" IDENTITY (1, 1)");
                    }
                    num++;
                    if (num != count)
                    {
                        builder.Append(",");
                        builder2.Append(",");
                        builder3.Append(",");
                    }
                    if (row2["definition"] != DBNull.Value)
                    {
                        builder4.Append(string.Concat(new object[] { "ALTER TABLE dbo.", row["name"].ToString(), " ADD CONSTRAINT DF_", row["name"].ToString(), "_", row2["name"], " DEFAULT ", row2["definition"].ToString(), " FOR [", row2["name"], "]\r \n" }));
                        builder5.Append(string.Concat(new object[] { "ALTER TABLE dbo.Tmp_", row["name"].ToString(), " ADD CONSTRAINT DF_Tmp_", row["name"].ToString(), "_", row2["name"], " DEFAULT ", row2["definition"].ToString(), " FOR [", row2["name"], "]\r \n" }));
                    }
                }
                builder.Append(")  ON [PRIMARY] \r \n");
                builder2.Append(")  ON [PRIMARY] \r \n");
                builder.Append(builder4.ToString());
                allDataByTableName = this.GetAllDataByTableName(row["name"].ToString());
                count = fieldByTableName.Rows.Count;
                foreach (DataRow row3 in allDataByTableName.Rows)
                {
                    builder.Append("INSERT INTO " + row["name"].ToString());
                    builder.Append(" VALUES (");
                    num2 = 0;
                    foreach (DataRow row2 in fieldByTableName.Rows)
                    {
                        num2++;
                        if (num2 != count)
                        {
                            if (row3[row2["name"].ToString()] == DBNull.Value)
                            {
                                builder.Append("NULL,");
                            }
                            else
                            {
                                builder.Append("'" + row3[row2["name"].ToString()].ToString().Replace("'", "''") + "',");
                            }
                        }
                        else if (row3[row2["name"].ToString()] == DBNull.Value)
                        {
                            builder.Append("NULL");
                        }
                        else
                        {
                            builder.Append("'" + row3[row2["name"].ToString()].ToString().Replace("'", "''") + "'");
                        }
                    }
                    builder.Append(")\r \n");
                    if (builder.Length > 0x30d40)
                    {
                        string str3;
                        string tmpfilename = this.tmpfilename;
                        string tmpnextfilename = this.tmpnextfilename;
                        this.tmpfilename = this.strtmpname.Replace("{i}", this.iCount.ToString());
                        this.tmpnextfilename = this.strtmpname.Replace("{i}", (this.iCount + 1).ToString());
                        if (this.iCount == 0)
                        {
                            str3 = "--@" + this.tmpnextfilename + "\n" + builder.ToString();
                            str3 = "--@Begin BackUp Data\n" + str3;
                            File.WriteAllText(HttpContext.Current.Server.MapPath(this.bakupPath + this.tmpfilename), str3);
                            builder = new StringBuilder();
                            this.strfilenamearr = this.tmpfilename + "," + this.tmpnextfilename;
                        }
                        else
                        {
                            this.strfilenamearr = this.strfilenamearr + "," + this.tmpnextfilename;
                            str3 = "--@" + this.tmpnextfilename + "\n" + builder.ToString();
                            File.WriteAllText(HttpContext.Current.Server.MapPath(this.bakupPath + tmpnextfilename), str3);
                            builder = new StringBuilder();
                        }
                        this.iCount++;
                    }
                    else
                    {
                        this.tmpnextfilename=this.strfilenamearr = this.strtmpname.Replace("{i}", this.iCount.ToString()); 
                    }
                }//end for
                if (flag)
                {
                    builder.Append(builder2.ToString());
                    builder.Append(builder5.ToString());
                    builder.Append("SET IDENTITY_INSERT dbo.Tmp_" + row["name"].ToString() + " ON\r \n");
                    builder.Append("IF EXISTS(SELECT * FROM dbo." + row["name"].ToString() + ") EXEC('INSERT INTO dbo.Tmp_" + row["name"].ToString() + " (" + builder3.ToString() + ") SELECT " + builder3.ToString() + " FROM dbo." + row["name"].ToString() + " WITH (HOLDLOCK TABLOCKX)')\r \n");
                    builder.Append("SET IDENTITY_INSERT dbo.Tmp_" + row["name"].ToString() + " OFF\r \n");
                    builder.Append("DROP TABLE dbo." + row["name"].ToString() + "\r \n");
                    builder.Append("EXECUTE sp_rename N'dbo.Tmp_" + row["name"].ToString() + "', N'" + row["name"].ToString() + "', 'OBJECT' \r \n");
                }
                builder.Append("\r \n\r \n");
            }
            string contents = "--@End BackUp Data\n" + builder.ToString();
            Random rnd = new Random();
            
            File.WriteAllText(HttpContext.Current.Server.MapPath(this.bakupPath + this.tmpnextfilename), contents);
            builder = null;
            builder2 = null;
            builder3 = null;
            builder4 = null;
            builder5 = null;
            GC.Collect();
        }

        /// <summary>
        /// ../databak_fa1fb2/
        /// </summary>
        /// <param name="path"></param>
        public string RunBackup()
        {

                string download = "";
                this.strtmpname = this.filename.Replace("{RoundNum}", this.GetRandNum(DateTime.Now.Second).ToString());
                this.backup();

                //string[] strArray = this.strfilenamearr.Split(new char[] { ',' });
                //for (int i = 0; i < strArray.Length; i++)
                //{
                //    download = string.Concat(new object[] { download, download, i, "：<a href="+this.bakupPath, strArray[i], " targe=_blank>", strArray[i], "</a><br/>" });
                //}
                string str3 = string.Empty;
                //str3 = downPath + this.strtmpname.Replace("_{i}.txt", "") + "_bak.zip";
                str3 = downPath + "SQLBackup_"+DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + "_bak.zip";
                ZipClass.Zip(HttpContext.Current.Server.MapPath(this.bakupPath), HttpContext.Current.Server.MapPath(str3), "");
                //download = download + "<br><br><a href='" + str3 + "' target='_blank' style='font-size:16px;font-weight:bold;'>备份压缩包下载</a>";
                download = "<br><br><a href='" + str3 + "' target='_blank' style='font-size:16px;font-weight:bold;'>备份压缩包下载</a>";

                //删除临时文件夹
                Directory.Delete(HttpContext.Current.Server.MapPath(this.bakupPath),true);
                return download;
        }

        private void Create()
        {
            SqlHelper.ExecuteNonQuery(("CREATE TABLE dbo.Table_1" + "(" + "id int NULL,") + "name nchar(10) NULL" + ")  ON [PRIMARY]");
        }

        private DataTable GetAllDataByTableName(string tablename)
        {
            return SqlHelper.ExecuteDataTable(string.Format("SELECT * FROM [{0}]",tablename));
        }

        private DataTable GetAllTable()
        {
            string str;
            if (this.getSqlVersion().IndexOf("2005") != -1 || this.getSqlVersion().IndexOf("2008") != -1)
            {
                str = "SELECT name FROM sysobjects WHERE (type = 'U') AND name <> 'sysdiagrams'";
            }
            else
            {
                str = "select name from sysobjects where xtype='u' and status>=0";
            }
            return SqlHelper.ExecuteDataTable(str);
        }

        private DataTable GetFieldByTableName(string tablename)
        {
            string str;
            if (this.getSqlVersion().Contains("2005") || this.getSqlVersion().Contains("2008"))
            {
                str = "SELECT     col.name, col.column_id, st.name AS type, col.max_length AS length, col.is_nullable,col.[precision], col.scale, col.is_identity, defCst.definition";
                str = ((((((str + " FROM         sys.columns AS col LEFT OUTER JOIN" + " sys.types AS st ON st.user_type_id = col.user_type_id LEFT OUTER JOIN") + " sys.types AS bt ON bt.user_type_id = col.system_type_id LEFT OUTER JOIN" + " sys.objects AS robj ON robj.object_id = col.rule_object_id AND robj.type = 'R' LEFT OUTER JOIN") + " sys.objects AS dobj ON dobj.object_id = col.default_object_id AND dobj.type = 'D' LEFT OUTER JOIN" + " sys.default_constraints AS defCst ON defCst.parent_object_id = col.object_id AND defCst.parent_column_id = col.column_id LEFT OUTER JOIN") + " sys.identity_columns AS idc ON idc.object_id = col.object_id AND idc.column_id = col.column_id LEFT OUTER JOIN" + " sys.computed_columns AS cmc ON cmc.object_id = col.object_id AND cmc.column_id = col.column_id LEFT OUTER JOIN") + " sys.fulltext_index_columns AS ftc ON ftc.object_id = col.object_id AND ftc.column_id = col.column_id LEFT OUTER JOIN" + " sys.xml_schema_collections AS xmlcoll ON xmlcoll.xml_collection_id = col.xml_collection_id") + " WHERE     (col.object_id = OBJECT_ID(N'dbo." + tablename + "'))") + " ORDER BY col.column_id";
            }
            else
            {
                str = "SELECT a.name,a.colorder,b.name as type,a.length,a.isnullable as is_nullable,COLUMNPROPERTY(a.id,a.name,'PRECISION') as [precision],COLUMNPROPERTY(a.id,a.name,'Scale') as scale,COLUMNPROPERTY(   a.id,a.name,'IsIdentity') as is_identity,e.text as definition  ";
                str = ((((str + "FROM   syscolumns   a   " + "left   join   systypes   b   on   a.xtype=b.xusertype   ") + "inner   join   sysobjects   d   on   a.id=d.id     and   d.xtype='U'   and     d.name<>'dtproperties'   " + "left   join   syscomments   e   on   a.cdefault=e.id   ") + "left   join   sysproperties   g   on   a.id=g.id   and   a.colid=g.smallid       " + "left   join   sysproperties   f   on   d.id=f.id   and   f.smallid=0   ") + "where   d.name='" + tablename + "' ") + "order by a.colorder";
            }
            return SqlHelper.ExecuteDataTable(str);
        }

        private string GetRandNum(int seed)
        {
            string str = string.Empty;
            Random random = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < 9; i++)
            {
                char ch;
                int num2 = random.Next();
                if ((num2 % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)((num2 - seed) % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)((num2 - seed) % 0x1a)));
                }
                str = str + ch.ToString().ToLower();
            }
            return str;
        }

        private string getSqlVersion()
        {
            DataTable table = SqlHelper.ExecuteDataTable("select @@Version as ver");
            if (table.Rows.Count > 0)
            {
                return table.Rows[0]["ver"].ToString();
            }
            return "";
        }

     }
}
