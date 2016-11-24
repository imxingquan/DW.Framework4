/**
 * (C)大连西数网络技术有限公司
 * 文件名: JavaScript.cs
 * 创建日期: 2007.5.20
 * 描述: 
 *       封装JavaScript脚本，实现一些如弹出对话框等的基本功能。
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;

namespace DW.Framework.Utils
{
    public class JavaScript
    {
        /// <summary>
        /// 弹出对话框
        /// </summary>
        /// <param name="msg">提示信息</param>
        public static void Alert(string msg)
        {
            HttpContext.Current.Response.Write("<script>alert('" + msg + "');</script>");
        }

        /// <summary>
        /// 弹出对话框
        /// </summary>
        /// <param name="page">要调用的页对象</param>
        /// <param name="msg">提示信息</param>
        public static void Alert(Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "alert", "alert('" + msg + "');",true);
        }
        
        /// <summary>
        /// 弹出对话框并后退
        /// </summary>
        /// <param name="msg">提示文字</param>
        public static void AlertBack(string msg)
        {
            HttpContext.Current.Response.Write("<script>alert('" + msg + "');history.back();</script>");
            HttpContext.Current.Response.End();
        }
        
        /// <summary>
        /// 弹出对话框并后退
        /// </summary>
        /// <param name="page">要调用的页对象</param>
        /// <param name="msg">提示文字</param>
        public static void AlertBack(Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(),"alertback","alert('" + msg + "');history.back();",true);
        }
        /// <summary>
        /// 弹出对话框并关闭
        /// </summary>
        /// <param name="msg">提示文字</param>
        public static void AlertClose(string msg)
        {
            HttpContext.Current.Response.Write("<script>alert('" + msg + "');window.close();</script>");
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 弹出对话框并转到指定页
        /// </summary>
        /// <param name="msg">提示文字</param>
        /// <param name="url">转到的Url地址</param>
        public static void AlertRedirect(string msg, string url)
        {
            HttpContext.Current.Response.Write("<script>alert('" + msg + "');window.location='" + url + "';</script>");
            HttpContext.Current.Response.End();
        }
    }
}
