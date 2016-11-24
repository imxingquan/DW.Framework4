/**
 * 大连西数网络技术有限公司 (C) 2007
 * 文件名: Helper.cs
 * 创建日期: 2007.5.20
 * 描述: 
 *       常用工具集合，一些未分组的工具集也暂时放到这里。
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Net;
using System.IO;

namespace DW.Framework.Utils
{
    /// <summary>
    /// 未整理的一些方法，暂时放到这里。
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// 返回当前用户的IP
        /// </summary>
        public static string CurrentUserIP
        {
            /*
             string ipAddress = "";
        if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null)
            ipAddress = HttpContext.Current.Request.ServerVariables["Remote_Addr"];
        else
            ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        return ipAddress;
             * */
            get { return HttpContext.Current.Request.UserHostAddress; }
        }


        /// <summary>
        /// 在指定的父级控件范围内，为Input控件添加onfocus和onblur属性，改变控件当得到焦点时的样式。
        /// </summary>
        public static void SetInputControlsHighlight(Control container, string className, bool onlyTextBoxes)
        {
            foreach (Control ctl in container.Controls)
            {
                if ((onlyTextBoxes && ctl is TextBox) || ctl is TextBox || ctl is DropDownList ||
                    ctl is ListBox || ctl is CheckBox || ctl is RadioButton ||
                    ctl is RadioButtonList || ctl is CheckBoxList)
                {
                    WebControl wctl = ctl as WebControl;
                    wctl.Attributes.Add("onfocus", string.Format("this.className = '{0}';", className));
                    wctl.Attributes.Add("onblur", "this.className = '';");
                }
                else
                {
                    if (ctl.Controls.Count > 0)
                        SetInputControlsHighlight(ctl, className, onlyTextBoxes);
                }
            }
        }


        /// <summary>
        /// 将输入的文档格式转换为HTML代码。<br/>
        /// </summary>
        /// <remarks>
        /// 在这里只针对如下转换
        /// "　" -> "&nbsp;&nbsp;" 全角空格转换成html代码的两个空格
        /// "\t" -> "&nbsp;&nbsp;&nbsp;"　制作表转换成html代码的三个空格
        /// "\n" -> "&ltbr&rt" 回车换成&ltbr&rt
        /// </remarks>
        public static string ConvertToHtml(string content)
        {
            content = HttpUtility.HtmlEncode(content);
            content = content.Replace("  ", "&nbsp;&nbsp;").Replace(
               "\t", "&nbsp;&nbsp;&nbsp;").Replace("\n", "<br>");
            return content;
        }

        /// <summary>
        /// 清除HTML标记
        /// </summary>
        public static string ClearHtml(string strHtml)
        {
            if (strHtml != "")
            {
                Regex r = null;
                Match m = null;

                r = new Regex(@"<\/?[^>]*>", RegexOptions.IgnoreCase);
                for (m = r.Match(strHtml); m.Success; m = m.NextMatch())
                {
                    strHtml = strHtml.Replace(m.Groups[0].ToString(), "");
                }
            }
            return strHtml;
        }

        public static string ClearScript(string html)
        {
            return html.Replace("<script", "&ltscript").Replace("</script","&rtscript").Replace("javascript", "");
        }
        /// <summary>
        /// 清除HTML标记不包含图片
        /// </summary>
        public static string ClearHtmlNotImg(string strHtml,string newStr)
        {
            if (strHtml != "")
            {
                Regex r = null;
                Match m = null;

                r = new Regex(@"<\/?[^(img)][^>]*>", RegexOptions.IgnoreCase);
                for (m = r.Match(strHtml); m.Success; m = m.NextMatch())
                {
                    strHtml = strHtml.Replace(m.Groups[0].ToString(), newStr);
                }
            }
            return strHtml;
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="n">数量</param>
        /// <param name="tail">截取之后接的字符串</param>
        /// <returns></returns>
        public static string SubStr(string str, int n, string tail)
        {
            if (string.IsNullOrEmpty(str))
                return "";

            string tempStr = ClearHtml(str);

            string reStr = "";//返回值

            //检测中英文
            if (tempStr.Length <= n / 2)
            {
                reStr = tempStr;
            }
            else
            {
                int t = 0;
                char[] tmp = tempStr.ToCharArray();
                for (int i = 0; i < tempStr.Length; i++)
                {
                    int c;
                    //c=Convert.ToInt32(tempStr.Substring(i,1));   
                    c = (int)tmp[i];
                    if (c < 0)
                        c = c + 65536;
                    if (c > 255)
                        t = t + 2;
                    else
                        t = t + 1;
                    if (t > n)
                        break;
                    reStr = reStr + tempStr.Substring(i, 1);
                }
                if (n < tempStr.Length)
                    reStr = reStr + tail;
            }
            return reStr;


            //不检测中英文
            //if (n >= tempStr.Length)
            //    n = tempStr.Length;

            //return tempStr.Substring(0, n);

        }


        public static void ShowErrorPage(string ErrMsg)
        {
#if WEB_TRACE
            HttpContext current = HttpContext.Current;
            current.Response.Write("<html>");
            current.Response.Write("<body style=\"font-size:14px;\">");
            current.Response.Write("System Error:<br />");
            current.Response.Write("<textarea name=\"errormessage\" style=\"width:95%; height:95%; word-break:break-all\">");
            current.Response.Write(ErrMsg);
            current.Response.Write("</textarea>");
            current.Response.Write("</body></html>");
            current.Response.End();
#endif
            System.Diagnostics.Debug.WriteLine(ErrMsg);
        }

        public static string ClsSql(string queryKey)
        {
            return queryKey.Replace("'", "''").Replace("\\", "").Replace("--","");
        }

        /// <summary>
        /// 标题链接转向
        /// </summary>
        /// <param name="url">链接地址</param>
        /// <param name="id">信息ID</param>
        /// <param name="regular">url为空时的规则，将信息ID替换规则内的{ID}</param>
        /// <returns></returns>
        public static string TitleRedirectUrl(string url, string id, string regular)
        {
            if (!string.IsNullOrEmpty(url))
                return url;

            return regular.IndexOf("{ID}") > 0 ? regular.Replace("{ID}", id) : regular + id;
        }

        public static string Domain
        {
            get
            {
                //string input = string.Empty;
                //string pattern = @"http://(?<domain>[^\/]*)";
                //input = HttpContext.Current.Request.Url.ToString();
                //Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                //string str3 = regex.Match(input).Groups["domain"].Value;
                //if (str3.ToLower().IndexOf("localhost") == -1)
                //{
                //   str3 = str3.ToLower().Replace("www.", "");
                //}
                //return str3;
                string host = System.Web.HttpContext.Current.Request.Url.Host.ToLower();
                if(host.ToLower().IndexOf("localhost") == -1) //not localhost
                {
                    host = host.Replace("www.","");
                    return host;
                }
                return "";
            }
        }

        /// <summary>
        /// 抓取网页数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="startStr"></param>
        /// <param name="endStr"></param>
        /// <returns></returns>
        public static  string SpiderHtml(string url, string startStr, string endStr)
        {
            HttpWebRequest httpWebRequest =
                  (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Accept = "*/*";
            httpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; CNCDialer; Maxthon; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
            HttpWebResponse httpWebResponse =
                    (HttpWebResponse)httpWebRequest.GetResponse();
            string contentType = httpWebResponse.ContentType;
            int pos = contentType.IndexOf("t=");
            if (pos == -1)
                contentType = "gb2312";
            else
                contentType = contentType.Substring(pos + 2);

            Stream stream = httpWebResponse.GetResponseStream();
            StreamReader streamReader =
               new StreamReader(stream, Encoding.GetEncoding(contentType));
            string html = streamReader.ReadToEnd();

            //查找起点
            int startPos = html.IndexOf(startStr);
            string getHtml = "";
            if (startPos > 0)
            {
                getHtml = html.Substring(startPos + startStr.Length);//截断找到位置之前的内容
                int endPos = getHtml.IndexOf(endStr);
                if (endPos > 0)
                {
                    getHtml = getHtml.Remove(endPos); //删除结束点之后的内容
                }
            }
            
            //如何长度太大说明有问题
            //if (ipName.Length > 200)
            //   return "found error!";
            return getHtml;
        }

        /// <summary>
        /// 根据起点和终点字符串截取
        /// </summary>
        /// <param name="input"></param>
        /// <param name="start_str"></param>
        /// <param name="end_str"></param>
        /// <returns></returns>
        public static string SubStr(string input, string start_str, string end_str)
        {
            //查找起点
            int startPos = input.IndexOf(start_str);
            string relStr = "";
            if (startPos > 0)
            {
                relStr = input.Substring(startPos + start_str.Length);//截断找到位置之前的内容
                int endPos = relStr.IndexOf(end_str);
                if (endPos > 0)
                {
                    relStr = relStr.Remove(endPos); //删除结束点之后的内容
                    return relStr;
                }
            }
            return "";
        }

        public static void SubStr(string input, string start_str, string end_str,IList<string> result)
        {
            int startPos = input.IndexOf(start_str);
            int endPos = input.IndexOf(end_str);
                        
            if (startPos > 0 && endPos > 0)
            {
                string relStr = input.Substring(startPos + start_str.Length);//截断找到位置之前的内容
                relStr = relStr.Remove(endPos); //删除结束点之后的内容
                result.Add(relStr);
                input = input.Substring(endPos + end_str.Length);
                SubStr(input, start_str, end_str, result);
            }
            return;
        }

        public static string GetWebPageName()
        {
            string filePath = HttpContext.Current.Request.FilePath;
            string str = filePath.Substring(filePath.LastIndexOf("/"));
            if (left(str, 1) == "/")
            {
                str = str.Substring(1);
            }
            return str;
        }
        public static string left(string str, int count)
        {
            if ((count != 0) && (str.Length > count))
            {
                str = str.Substring(0, count);
            }
            return str;
        }

    }
}
