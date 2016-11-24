/********************************************8
 * 配置类加载
 * 2012-5-31
 * im@xingquan.org
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DW.Framework.Data;

namespace DW.Framework.Configuration
{

    public class ConfigManager<T> where T : IDataProvider //参数约束：必须是IDataProvider或他的实现
    {
        public static ConfigManager<T> Current = new ConfigManager<T>();

        private string fileName;
        public XmlConfigFile xml;

        public ConfigManager()
        {

            fileName = typeof(T).Assembly.ManifestModule.ToString() + ".config";
            xml = new XmlConfigFile(AppDomain.CurrentDomain.BaseDirectory, fileName);
        }
         
        /// <summary>
        /// 根据T类型名称获取配置文件对应的实例化类型名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public string GetProviderType()  
        {
            string key = typeof(T).Name;

            if (!xml.Elements.ProviderTypes.ContainsKey(key))
                throw new Exception(string.Format("检查配置文件是否有{0}接口对应的实例化类型字符串。",key));
            return xml.Elements.ProviderTypes[key]; 
        }

        public string ConnectionString
        {
            get
            {
                return xml.Elements.ConnectionString;
            }
        }

    }
}
