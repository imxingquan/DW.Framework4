using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DW.Framework.Utils
{
    public class RssToolkit
    {

        #region 读取RSS函数LoadRSS
        /// <summary>
        /// 读取RSS函数LoadRSS
        /// </summary>
        /// <param name="RssUrl">RSS路径</param>
        /// <param name="RssCount">读取的RSS条数</param>
        /// <returns></returns>
        public static List<RssItem> LoadRSS(string RssUrl, int RssCount)
        {
            if (string.IsNullOrEmpty(RssUrl))
                throw new Exception("地址为空");

            XmlDocument doc = new XmlDocument();
            XmlReader xmlReader = new XmlTextReader(RssUrl);
            List<RssItem> rssItems = new List<RssItem>();

            if (!xmlReader.Read()) throw new Exception( "错误的地址!");

                try
                {

                    doc.Load(xmlReader);
                    XmlNodeList nodelist = doc.GetElementsByTagName("item");
                    XmlNodeList objItems1;
                    int i = 0;
                    if (doc.HasChildNodes)
                    {
                        foreach (XmlNode node in nodelist)
                        {
                            string title = "";
                            string link = "";
                            string pubDate = "";
                            string description = "";
                            string author = "";

                            i += 1;
                            if (node.HasChildNodes)
                            {
                                objItems1 = node.ChildNodes;
                                foreach (XmlNode childNode in objItems1)
                                {
                                    switch (childNode.Name.ToLower())
                                    {
                                        case "title":
                                            title = childNode.InnerText.Trim();
                                            break;
                                        case "link":
                                            link = childNode.InnerText.Trim();
                                            break;
                                        case "pubdate":
                                            pubDate = childNode.InnerText.Trim();
                                            break;
                                        case "description":
                                            description = childNode.InnerText.Trim();
                                            break;
                                        case "author":
                                            author = childNode.InnerText.Trim();
                                            break;

                                    }
                                    if (title != "" && link != "" && pubDate != "" && description != "")
                                        break;
                                }
                                rssItems.Add(new RssItem(title, link, author, description, pubDate));
                                //Rss += "<div style=\"height:22px;\"><span style=\"width:350px;\"><a href='" + link + "' target='_blank'>" + CfgUtl.SubStr(title, 55, "") + "</a></span> " + Convert.ToDateTime(pubDate).ToShortDateString() + "</div>";
                            }
                            if (RssCount != 0)
                                if (i > RssCount - 1)
                                    break;
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("源数据错误！");
                }
           
           
            return rssItems;
        }
        #endregion

    }

    public class RssItem
    {
        #region Meta Data

        private string _title = "";

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _link = "";

        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }

        private string _author = "";

        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }

        private string _description = "";

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _pubDate = "";

        public string PubDate
        {
            get { return _pubDate; }
            set { _pubDate = value; }
        }

        #endregion


        public RssItem()
        {
        }

        public RssItem(string title, string link)
        {
            _title = title;
            _link = link;
        }

        public RssItem(string title, string link, string author, string description, string pubDate)
        {
            _title = title;
            _link = link;
            _author = author;
            _description = description;
            _pubDate = pubDate;
        }
    }
}
