using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using DW.Framework.Configuration;
using System.Collections;

namespace DW.Framework.Pager
{
    public static class Page
    {
      
        public static string GetPageNumbers(int curPage, int countPage, string url, int extendPage)
        {
            //直接获取url地址的参数
            if (string.IsNullOrEmpty(url))
            {
                url = HttpContext.Current.Request.Url.Query;
                url = url.Replace("&page=" + curPage, "");
                url = url.Replace("?page=" + curPage, "");
            }


            int startPage = 1;
            int endPage = 1;


            if (url.StartsWith("?") == true)
            {
                url = url + "&";
            }
            else
            {
                url = url + "?";
            }


            //第一页（首页）
            string t1 = "<a href=\"" + url + "page=1" + "\">第一页</a>";
            //最后一页（末页）
            string t2 = "<a href=\"" + url + "page=" + countPage + "\">最后一页</a>";
            //上一页（前一页）
            string t3 = "<a href=\"" + url + "page=" + (curPage - 1) + "\">上一页</a>";
            //下一页
            string t4 = "<a href=\"" + url + "page=" + (curPage + 1) + "\">下一页</a>";

            if (countPage < 1) countPage = 1;
            if (extendPage < 3) extendPage = 2;

            if (countPage > extendPage)
            {
                if (curPage - (extendPage / 2) > 0)
                {
                    if (curPage + (extendPage / 2) < countPage)
                    {
                        startPage = curPage - (extendPage / 2);
                        endPage = startPage + extendPage - 1;
                    }
                    else
                    {
                        endPage = countPage;
                        startPage = endPage - extendPage + 1;
                        //t2 = "<span class=\"nolink\">最后一页</span>";
                        t2 = "";
                    }
                }
                else
                {
                    endPage = extendPage;
                    //t1 = "<span class=\"nolink\">第一页</span>";
                    t1 = "";
                }

            }
            else
            {
                startPage = 1;
                endPage = countPage;
                //t1 = "<span class=\"nolink\">第一页</span>";
                //t2 = "<span class=\"nolink\">最后一页</span>";
                t1 = "";
                t2 = "";
            }

            if (curPage <= 1)
                t3 = "";  //t3 = "<span class=\"nolink\">上一页</span>";

            if (curPage >= countPage)
                t4 = "";// t4 = "<span class=\"nolink\">下一页</span>";

            StringBuilder s = new StringBuilder("");

            s.Append(t1);
            s.Append(t3);
            for (int i = startPage; i <= endPage; i++)
            {
                if (i == curPage)
                {
                    s.Append("<span class=\"curPage\">");
                    s.Append(i);
                    s.Append("</span>");
                }
                else
                {
                    s.Append("<a href=\"");
                    s.Append(url);
                    s.Append("page=");
                    s.Append(i);
                    s.Append("\">");
                    s.Append(i);
                    s.Append("</a>");
                }
            }
            s.Append(t4);
            s.Append(t2);

            return s.ToString();
        }

        public static string GetPageHtmlNumbers(int curPage, int countPage, string url, int extendPage)
        {

            int startPage = 1;
            int endPage = 1;


            //第一页（首页）
            string t1 = "<a href=\"" + url.Replace("#page", "1") + "\">第一页</a>";
            //最后一页（末页）
            string t2 = "<a href=\"" + url.Replace("#page", countPage.ToString()) + "\">最后一页</a>";
            //上一页（前一页）
            string t3 = "<a href=\"" + url.Replace("#page", (curPage - 1).ToString()) + "\">上一页</a>";
            //下一页
            string t4 = "<a href=\"" + url.Replace("#page", (curPage + 1).ToString()) + "\">下一页</a>";

            if (countPage < 1) countPage = 1;
            if (extendPage < 3) extendPage = 2;

            if (countPage > extendPage)
            {
                if (curPage - (extendPage / 2) > 0)
                {
                    if (curPage + (extendPage / 2) < countPage)
                    {
                        startPage = curPage - (extendPage / 2);
                        endPage = startPage + extendPage - 1;
                    }
                    else
                    {
                        endPage = countPage;
                        startPage = endPage - extendPage + 1;
                        //t2 = "<span class=\"nolink\">最后一页</span>";
                        t2 = "";
                    }
                }
                else
                {
                    endPage = extendPage;
                    //t1 = "<span class=\"nolink\">第一页</span>";
                    t1 = "";
                }

            }
            else
            {
                startPage = 1;
                endPage = countPage;
                //t1 = "<span class=\"nolink\">第一页</span>";
                //t2 = "<span class=\"nolink\">最后一页</span>";
                t1 = "";
                t2 = "";
            }

            if (curPage <= 1)
                t3 = "";// t3 = "<span class=\"nolink\">上一页</span>";

            if (curPage >= countPage)
                t4 = "";// t4 = "<span class=\"nolink\">下一页</span>";

            StringBuilder s = new StringBuilder("");

            s.Append(t1);
            s.Append(t3);
            for (int i = startPage; i <= endPage; i++)
            {
                if (i == curPage)
                {
                    s.Append("<span class=\"curPage\">");
                    s.Append(i);
                    s.Append("</span>");
                }
                else
                {
                    s.Append("<a href=\"");
                    s.Append(url.Replace("#page", i.ToString()));
                    s.Append("\">");
                    s.Append(i);
                    s.Append("</a>");
                }
            }
            s.Append(t4);
            s.Append(t2);

            return s.ToString();
        }

        //WebForm
        //直接使用list.PagerNum
        public static string PagerNum(this IPaged pager, string url = "", int extendPage = 5,bool isHtml = false)
        {
            //读取配置文件PageSize
            //if (pager.PageSize == MaxRows.ConfigPageSize)
            //    pager.PageSize = ConfigManager.Current.PageSize;

            if (pager.TotalItemCount <= pager.PageSize)
                return "";
        

            string pageStr = "";
            if (isHtml)
                pageStr = GetPageHtmlNumbers(pager.PageIndex, pager.TotalPageCount, url, extendPage);
            else
                pageStr = GetPageNumbers(pager.PageIndex, pager.TotalPageCount, url, extendPage);

            return string.Format("<span class=\"text\">页次:{0}/{1} 每页:{2} 条 共:{3} 条</span>{4}", pager.PageIndex, pager.TotalPageCount, pager.PageSize, pager.TotalItemCount, pageStr);
        }

        public static string PagerNum(this IPaged pager)
        {
            return PagerNum(pager, "", 5, false);
        }
    }
}
