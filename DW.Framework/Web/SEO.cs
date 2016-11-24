using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace DW.Framework.Web
{
    public class SEO
    {
        public static void AddTitle(Page page, string title)
        {
            page.Title = title;
        }

        /// <summary>
        /// 添加META信息
        /// </summary>
        /// <param name="page">Page对象</param>
        /// <param name="key">keywords,description</param>
        /// <param name="content">内容</param>
        public static void AddMeta(Page page, string key, string content)
        {
            HtmlMeta hm1 = new HtmlMeta();

            // Define an HTML <meta> element that is useful for search engines.
            hm1.Name = key;
            hm1.Content = content;

            page.Header.Controls.Add(hm1);

        }

    }
}
