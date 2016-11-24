using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DW.Framework.Configuration
{
    public class ConfigSection
    {
        private string _connectionString;
        /// <summary>
        /// 数据库连接
        /// </summary>
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        private Dictionary<string,string> _providerTypes;
        /// <summary>
        /// 数据源提供者
        /// </summary>
        public Dictionary<string, string> ProviderTypes
        {
            get { return _providerTypes; }
            set { _providerTypes = value; }
        }
    }
}
