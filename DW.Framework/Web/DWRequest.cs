using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DW.Framework.Web
{
    public class DWRequest
    {
        public static string QueryString(string name)
        {
            string q =String.Empty;
            if( HttpContext.Current.Request.QueryString[name] !=null)
            {
                q = ChkStr(HttpContext.Current.Request.QueryString[name]);
            }
            return q;
        }

        public static string ChkStr(string str)
        {
           return  str = str.Replace("'","''").Replace("--","");
        }

        public static string Request(string name)
        {
            string q = String.Empty;
            if (HttpContext.Current.Request[name] != null)
            {
               q = ChkStr(HttpContext.Current.Request[name]);
            }
            return q;
        }

        public static string Request(string name, bool encodedecode = false)
        {
            string q = String.Empty;
            if (HttpContext.Current.Request[name] != null)
            {
                if (encodedecode)
                    q = ChkStr(DW.Framework.Security.EncodeDecode.JavaScriptEncode(HttpContext.Current.Request[name]));
                else
                    q = ChkStr(HttpContext.Current.Request[name]);
            }
            return q;
        }

       
        public static bool Request(string key,out int result)
        {
            result = -1;
            return HttpContext.Current.Request[key] != null && int.TryParse(HttpContext.Current.Request[key], out result);
        }

        /// <summary>
        /// 获取翻页参数
        /// </summary>
        public static int ReqPage(string key)
        {
            int result = 1;
            if (HttpContext.Current.Request[key] != null && int.TryParse(HttpContext.Current.Request[key], out result))
            {
                return result;
            }
            return 1;
        }

        public static string RequestInt(string key)
        {
            int result;
            if (HttpContext.Current.Request[key] == null || 
                int.TryParse(HttpContext.Current.Request[key], out result) == false)
            {
                //转到错误页面
                throw new Exception(string.Format("[{0}] :url 参数错误,应该为int类型",key));
            }

            return result.ToString();
            
        }

        public static int RequestInt32(string key)
        {
            return Convert.ToInt32(RequestInt(key));
        }

        /// <summary>
        /// 获取url参数的int值，如果获取失败或不存在返回给定的默认值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int RequestInt32(string key, int defaultValue)
        {
            //如果不存在值返回默认值
            if (HttpContext.Current.Request[key] == null)
                return defaultValue;
            else
                return RequestInt32(key);

        }
    }
}
