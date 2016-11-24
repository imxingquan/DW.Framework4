/**
 * (C)大连西数网络技术有限公司
 * 文件名: SqlInject.cs
 * 创建日期: 2008.12.27
 * 描述: 
 *       SQL防注入
 * 
 */
using System;
using System.Configuration;
using System.Web;
using System.Globalization;

namespace DW.Framework.Security
{
    /// <summary>
    /// SQL防注入
    /// </summary>
    public class SqlstrAny : IHttpModule
    {

        #region IHttpModule 成员

        public void Init(HttpApplication application)
        {
            application.BeginRequest += (new
            EventHandler(this.Application_BeginRequest));
        }
        private void Application_BeginRequest(Object source, EventArgs e)
        {
            ProcessRequest pr = new ProcessRequest();
            pr.StartProcessRequest();
        }
        public void Dispose()
        {
        }

        #endregion

     
    }

    public class ProcessRequest
    {
        private static string SqlStr = ConfigurationManager.AppSettings["SqlInject"].ToString();

        private static string SqlStrForm = ConfigurationManager.AppSettings["SqlInjectForm"].ToString();

        private static string sqlErrorPage = ConfigurationManager.AppSettings["SQLInjectErrPage"].ToString();
        ///
        /// 用来识别是否是流的方式传输
        ///
        ///
        ///
        bool IsUploadRequest(HttpRequest request)
        {
            return StringStartsWithAnotherIgnoreCase(request.ContentType, "multipart/form-data");
        }
        ///
        /// 比较内容类型
        ///
        ///
        ///
        ///
        private static bool StringStartsWithAnotherIgnoreCase(string s1, string s2)
        {
            return (string.Compare(s1, 0, s2, 0, s2.Length, true, CultureInfo.InvariantCulture) == 0);
        }

        //SQL注入式攻击代码分析
        #region SQL注入式攻击代码分析
        ///
        /// 处理用户提交的请求
        ///
        public void StartProcessRequest()
        {
            HttpRequest Request = System.Web.HttpContext.Current.Request;
            HttpResponse Response = System.Web.HttpContext.Current.Response;
            try
            {
                string getkeys = "";
                if (IsUploadRequest(Request)) return; //如果是流传递就退出
                //字符串参数
                if (Request.QueryString != null )
                {
                    for (int i = 0; i < Request.QueryString.Count; i++)
                    {
                        string errStr;
                        getkeys = Request.QueryString.Keys[i];
                        if (getkeys == "errmsg")
                            break;

                        if (!ProcessSqlStr(Request.QueryString[getkeys],out errStr))
                        {
                            Response.Redirect(sqlErrorPage + "?errmsg=QueryString中含有非法字符串" + errStr + "&sqlprocess=true");
                            Response.End();
                        }
                    }
                }
                //form参数
                if (Request.Form != null)
                {
                    for (int i = 0; i < Request.Form.Count; i++)
                    {
                        getkeys = Request.Form.Keys[i];
                        string errStr = "";
                        if (!ProcessSqlStrForm(Request.Form[getkeys],out errStr))
                        {
                            Response.Redirect(sqlErrorPage + "?errmsg=Form中含有非法字符串"+errStr+"&sqlprocess=true");
                            Response.End();
                        }
                    }
                }
                //cookie参数
                if (Request.Cookies != null)
                {
                    for (int i = 0; i < Request.Cookies.Count; i++)
                    {
                        string errStr;
                        getkeys = Request.Cookies.Keys[i];
                        if (!ProcessSqlStr(Request.Cookies[getkeys].Value,out errStr))
                        {
                            Response.Redirect(sqlErrorPage + "?errmsg=Cookie中含有非法字符串" + errStr + "&sqlprocess=true");
                            Response.End();
                        }
                    }
                }
            }
            catch
            {
                // 错误处理: 处理用户提交信息!
                Response.Clear();
                Response.Write("CustomErrorPage配置错误");
                Response.End();
            }
        }

        ///
        /// 分析用户请求是否正常
        ///
        /// 传入用户提交数据
        /// 返回是否含有SQL注入式攻击代码
        private bool ProcessSqlStr(string Str,out string errStr)
        {
            errStr="";
            if (string.IsNullOrEmpty(SqlStr))
                return true;

            bool ReturnValue = true;
            try
            {
                if (Str != "")
                {
                    string[] anySqlStr = SqlStr.Split('|');
                    foreach (string ss in anySqlStr)
                    {
                        if (Str.IndexOf(ss) >= 0)
                        {   errStr=ss;
                            ReturnValue = false;
                            break;
                        }
                    }
                }
            }
            catch
            {
                ReturnValue = false;
            }
            return ReturnValue;
        }

        ///
        /// 分析用户请求是否正常
        ///
        /// 传入用户提交数据
        /// 返回是否含有SQL注入式攻击代码
        private bool ProcessSqlStrForm(string Str,out string errStr)
        {
            errStr = "";
            if (string.IsNullOrEmpty(SqlStrForm))
                return true;

            bool ReturnValue = true;
            try
            {
                if (Str != "")
                {
                    string[] anySqlStr = SqlStrForm.ToLower().Split('|');
                    foreach (string ss in anySqlStr)
                    {
                        if (Str.ToLower().Contains(ss))
                        {
                            errStr = ss;
                            ReturnValue = false;
                            break;
                        }
                    }
                }
            }
            catch
            {
                ReturnValue = false;
            }
            return ReturnValue;
        }
        #endregion
    }


}
