using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;
using System.IO;
using System.Xml.XPath;

namespace DW.Framework.Configuration
{
    public sealed class XmlConfig
    {
        private  XmlDocument xmlDoc;

        private  string xmlFile;
              
        //不能创建此类的对象
        public XmlConfig(string path,string fileName)
        {
            xmlFile = Path.Combine(path, fileName);

        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        public string GetConnectionStrings(string name = "defaultConnection")
        {
            string retVal = "";
            XmlElement xmlElement;

            // 找到<connectionStrings>节
            XmlNode xmlConnectionStrings = xmlDoc.SelectSingleNode("//connectionStrings");

            if (xmlConnectionStrings == null)
                throw new XPathException("没有connectonStrings配置节!");

            //获取节点上的 key 属性
            XmlNode xmlNode = xmlConnectionStrings.SelectSingleNode(("//add[@name='" + name + "']"));

            if (xmlNode == null)
                throw new XPathException("ConnectionStrings配置节没有找到 [" + name + "]");

            xmlElement = (XmlElement)xmlNode;
            retVal = xmlElement.GetAttributeNode("connectionString").Value;

            return retVal;
        }

        /// <summary>
        /// 获取缓存开关
        /// </summary>
        /// <returns></returns>
        public  bool GetEnableCaching()
        {
            return Convert.ToBoolean(GetAppSetting("EnableCaching"));
        }

        /// <summary>
        /// 获取缓存失效时间
        /// </summary>
        /// <returns></returns>
        public  int GetCacheDuration()
        {
            return Convert.ToInt32(GetAppSetting("CacheDuration"));
        }

        /// <summary>
        /// 返回每页行数
        /// </summary>
        /// <returns></returns>
        public  int GetPageSize()
        {
            return Convert.ToInt32(GetAppSetting("PageSize"));
        }

        /// <summary>
        /// 根据Key获取Value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public  string GetValue(string key)
        {
            return GetAppSetting(key);
        }
        /// <summary>
        /// 返回类型配置
        /// </summary>
        /// <returns></returns>
        public  Dictionary<string, string> GetProviderTypes()
        {
            
            XmlElement xmlElement;
            Dictionary<string, string> dataProviderDict = new Dictionary<string, string>();
            try
            {
                XmlNode xmlDataProvider = xmlDoc.SelectSingleNode("//DataProvider");

                foreach (XmlNode node in xmlDataProvider.ChildNodes)
                {
                    //跳过注释
                    if (node is XmlComment) continue;

                    xmlElement = (XmlElement)node;
                    string key = xmlElement.GetAttributeNode("interface").Value;
                    string value = xmlElement.GetAttributeNode("providerType").Value;
                    if (!dataProviderDict.ContainsKey(key))
                        dataProviderDict.Add(key, value);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("配置错误GetProviderTypes读取错误" + ex.Message);
            }
            return dataProviderDict;
        }
        /// <summary>
        /// 载入XML文件
        /// </summary>
        /// <param name="fileName">XML的文件名</param>
        /// <returns>返回XmlDocument对象</returns>
        public  XmlDocument LoadXml()
        {
            if (!File.Exists(xmlFile))
                throw new FileNotFoundException("file " + xmlFile + " not found!");
            
            //打开配置文件
            xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFile);
            return xmlDoc;
        }

       
        /// <summary>
        /// 获取文件的最后修改时间
        /// </summary>
        /// <returns></returns>
        public  DateTime GetLastWriteTime()
        {
            if (!File.Exists(xmlFile))
                throw new FileNotFoundException("配置文件 [" + xmlFile + "] 没有找到");

            return File.GetLastWriteTime(xmlFile);
        }

        private  string GetAppSetting(string key)
        {
            string retVal = "";

            XmlElement xmlElement;

            // retrieve the appString node
            XmlNode xmlAppSettings = xmlDoc.SelectSingleNode("//appSettings");

            

            if (xmlAppSettings == null)
                throw new XPathException("没有找到appSettings配置");

            //获取节点上的 key 属性
            XmlNode xmlNode = xmlAppSettings.SelectSingleNode(("//add[@key='" + key + "']"));

            if (xmlNode == null)
                throw new XPathException("没有找到 [" + key + "] 的配置信息");

            xmlElement = (XmlElement)xmlNode;

            retVal = xmlElement.GetAttributeNode("value").Value;

            return retVal;
        }

        /// <summary>
        /// 为XML文档中的 appSettings 节添加配置
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">key对应的值</param>
        /// <returns>返回已经添加完节的XmlDocument对象</returns>
        /// <example>
        /// <code>
        /// </code>
        /// </example>
        private  XmlDocument AddAppSetting(string key, string value)
        {
            XmlElement xmlElement;

            // retrieve the appString node
            XmlNode xmlAppSettings = xmlDoc.SelectSingleNode("//appSettings");

            if (xmlAppSettings != null)
            {
                // get the node based on key
                //获取节点上的 key 属性
                XmlNode xmlNode = xmlAppSettings.SelectSingleNode(("//add[@key='" + key + "']"));

                if (xmlNode != null)    //如果 key 存在重写 value 的值
                {
                    //update the existing element
                    xmlElement = (XmlElement)xmlNode;
                    xmlElement.SetAttribute("value", value);
                }
                else       //添加新的 key/value 对到 appSettings节点下。
                {
                    xmlElement = xmlDoc.CreateElement("add");
                    xmlElement.SetAttribute("key", key);
                    xmlElement.SetAttribute("value", value);
                    xmlAppSettings.AppendChild(xmlElement);
                }
                Save(xmlDoc);
            }

            // return the xml doc
            return xmlDoc;
        }

        /// <summary>
        /// 保存XML文档
        /// </summary>
        /// <param name="xmlDoc">要保存的XmlDocument对象</param>
        /// <returns>如果保存成功返回空字符串，否则返回错误信息。</returns>
        private string Save(XmlDocument xmlDoc)
        {
            if (!File.Exists(xmlFile))
                throw new FileNotFoundException("file " + xmlFile + " not found!");

            try
            {
                XmlTextWriter writer = new XmlTextWriter(xmlFile, null);
                writer.Formatting = Formatting.Indented;
                xmlDoc.WriteTo(writer);
                writer.Flush();
                writer.Close();
                return "";
            }
            catch (Exception exc)
            {
                return exc.Message;
            }
        }

        public void WriteValue(string key, string value)
        {
            AddAppSetting(key, value);
        }
    }
}
