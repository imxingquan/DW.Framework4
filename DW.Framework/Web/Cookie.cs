using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DW.Framework.Security;

namespace DW.Framework.Web
{
    public class Cookie
    {
        private string selfKey = "sdf23$%^#@#423sdf45k23dsf  sdf 34[ 0esf XING";

        private string COOKIES_ID;
        private bool encrpy;
        
        
        public Cookie(string cookies_id,string value=null,bool encrpy = false)
        {   
            this.encrpy = encrpy;
            this.COOKIES_ID = cookies_id;
            if (!string.IsNullOrEmpty(value))
                this.Value = value;
        }

        /// <summary>
        /// 写入或获取Cookies的值，如果失败返回 nul
        /// </summary>
        public string Value
        {
            set
            {
                HttpContext.Current.Response.Cookies[COOKIES_ID].Domain = DW.Framework.Utils.Helper.Domain;
                //HttpContext.Current.Response.Cookies[COOKIES_ID].Expires = DateTime.Now.AddDays(30);
                if (encrpy)
                {
                    HttpContext.Current.Response.Cookies[COOKIES_ID].Value = EncodeCookie(value);
                }
                else
                {
                    HttpContext.Current.Response.Cookies[COOKIES_ID].Value = value;
                }
            }
            get
            {
                if (HttpContext.Current.Request.Cookies[COOKIES_ID] != null)
                {
                    if (encrpy)
                        return DecodeCookie(HttpContext.Current.Request.Cookies[COOKIES_ID].Value);
                    else
                        return HttpContext.Current.Request.Cookies[COOKIES_ID].Value;
                }
                return null;
            }
                       
        }
        
        /// <summary>
        /// 检测cookies是否有效
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            if (HttpContext.Current.Request.Cookies[COOKIES_ID] != null
                && !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies[COOKIES_ID].Value))
                return true;
            return false;
        }

        /// <summary>
        /// 清除cookies
        /// </summary>
        public void Clear()
        {
            if (HttpContext.Current.Request.Cookies[COOKIES_ID] != null)
            {
                //HttpContext.Current.Response.Cookies[COOKIES_ID].Expires = DateTime.Now.AddDays(-10);
                //HttpContext.Current.Response.Cookies.Remove(COOKIES_ID);
                //HttpContext.Current.Response.Cookies.Clear();
                //用下面的方法才可以退出cookies
                HttpCookie cookieID = new HttpCookie(COOKIES_ID);
                cookieID.Expires = DateTime.Now.AddHours(-24);
                HttpContext.Current.Response.Cookies.Add(cookieID);
                
            }
        }


        #region private method
        /// <summary>
        /// 解密cookies
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string DecodeCookie(string str)
        {
            return EncrpytFactory.CreateInstance(selfKey).Decrypt(str);
        }

        /// <summary>
        /// 加密cookies
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string EncodeCookie(string str)
        {
            return EncrpytFactory.CreateInstance(selfKey).Encrypt(str);
        }

        #endregion

        #region static method
        /// <summary>
        /// 设置Cookies的值
        /// </summary>
        /// <param name="cookies_id"></param>
        /// <param name="value"></param>
        public static void SetValue(string cookies_id, string value,bool encrpy=false)
        {
            Cookie myCookie = new Cookie(cookies_id,value,encrpy);
            //myCookie.Value = value;
        }
        /// <summary>
        /// 获取Cookies的值
        /// </summary>
        /// <param name="cookies_id"></param>
        /// <returns></returns>
        public static string GetValue(string cookies_id,bool encrpy=false)
        {
            Cookie cookie = new Cookie(cookies_id,null,encrpy);
            return cookie.Value!=null ? cookie.Value : null ;
        }

        /// <summary>
        /// 检测cookies是否有效
        /// </summary>
        public static bool IsValid(string cookies_id)
        {
            return new Cookie(cookies_id).IsValid();
        }

        public static void Clear(string cookies_id)
        {
            new Cookie(cookies_id).Clear();
        }
        #endregion
    }
}
