/**
 * (C)�����������缼�����޹�˾
 * �ļ���: JavaScript.cs
 * ��������: 2007.5.20
 * ����: 
 *       ��װJavaScript�ű���ʵ��һЩ�絯���Ի���ȵĻ������ܡ�
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
        /// �����Ի���
        /// </summary>
        /// <param name="msg">��ʾ��Ϣ</param>
        public static void Alert(string msg)
        {
            HttpContext.Current.Response.Write("<script>alert('" + msg + "');</script>");
        }

        /// <summary>
        /// �����Ի���
        /// </summary>
        /// <param name="page">Ҫ���õ�ҳ����</param>
        /// <param name="msg">��ʾ��Ϣ</param>
        public static void Alert(Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "alert", "alert('" + msg + "');",true);
        }
        
        /// <summary>
        /// �����Ի��򲢺���
        /// </summary>
        /// <param name="msg">��ʾ����</param>
        public static void AlertBack(string msg)
        {
            HttpContext.Current.Response.Write("<script>alert('" + msg + "');history.back();</script>");
            HttpContext.Current.Response.End();
        }
        
        /// <summary>
        /// �����Ի��򲢺���
        /// </summary>
        /// <param name="page">Ҫ���õ�ҳ����</param>
        /// <param name="msg">��ʾ����</param>
        public static void AlertBack(Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(),"alertback","alert('" + msg + "');history.back();",true);
        }
        /// <summary>
        /// �����Ի��򲢹ر�
        /// </summary>
        /// <param name="msg">��ʾ����</param>
        public static void AlertClose(string msg)
        {
            HttpContext.Current.Response.Write("<script>alert('" + msg + "');window.close();</script>");
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// �����Ի���ת��ָ��ҳ
        /// </summary>
        /// <param name="msg">��ʾ����</param>
        /// <param name="url">ת����Url��ַ</param>
        public static void AlertRedirect(string msg, string url)
        {
            HttpContext.Current.Response.Write("<script>alert('" + msg + "');window.location='" + url + "';</script>");
            HttpContext.Current.Response.End();
        }
    }
}
