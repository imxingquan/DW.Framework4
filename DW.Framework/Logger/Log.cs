using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace DW.Framework.Logger
{
    public delegate void LogTrace(string message);

    public class Log
    {
        public static event LogTrace EventTrace;

        public static void Write(string msg)
        {
#if DEBUG
            Debug.WriteLine(msg);
        #if WEB_TRACE
            
            if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Trace.IsEnabled)
            {
                string url = System.Web.HttpContext.Current.Request.RawUrl;
                string err = url + "<br/>" + msg;
                System.Web.HttpContext.Current.Trace.Warn("Error:", err);

                DW.Framework.Utils.Helper.ShowErrorPage(msg);
            }
        #endif  
#else

            try
            {   
                string url="";
                if (System.Web.HttpContext.Current != null)
                {
                    url = System.Web.HttpContext.Current.Request.RawUrl; 
                    if(url != ""){
                        msg = "-->"+url +"\r\n   "+msg;
                               
                    }
                    DW.Framework.Utils.Helper.ShowErrorPage(msg);    
                }
                
                System.IO.StreamWriter sw = System.IO.File.AppendText(AppDomain.CurrentDomain.BaseDirectory + "/" + DateTime.Today.ToString("yyyy_MM_dd") + "_log.txt");
                lock(sw)
                {
                    sw.WriteLine(DateTime.Now.ToString() + "：");
                    sw.WriteLine(msg + "\r\n");
                    sw.Flush();
                    sw.Close();
                }
            }
            finally { }
#endif
        }

        public static void Write(object obj)
        {
            try
            {
                System.IO.StreamWriter sw = System.IO.File.AppendText(AppDomain.CurrentDomain.BaseDirectory + "/" + DateTime.Today.ToString() + "_log.txt");

                sw.WriteLine(DateTime.Now.ToString() + "：");
                sw.WriteLine(obj.ToString() + "\r\n");
                sw.Flush();
                sw.Close();
            }
            finally { }
        }

        /// <summary>
        /// 如果定义 WEB_TRACE，将调试输出到asp.net页面的，页面打开trace="true"开关
        /// </summary>
        /// <param name="sql"></param>
        public static void Trace(string sql)
        {            
            Debug.WriteLine(sql);

#if WEB_TRACE 
          
            if(System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Trace.IsEnabled)
                System.Web.HttpContext.Current.Trace.Warn("SQL",sql);

            //if (System.Web.HttpContext.Current != null)
            //    System.Web.HttpContext.Current.Response.Write("<span style='color:red'>" + sql + "</span><br/>");
#endif

            if (EventTrace != null)
                EventTrace(sql);
        }
    }
}
