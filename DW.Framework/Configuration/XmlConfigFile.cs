using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DW.Framework.Configuration
{
    public class XmlConfigFile : XmlLoadBase
    {
        public ConfigSection Elements;

        public XmlConfigFile(string path,string file)
            :base(path,file)
        {
            Load();
        }

        protected override void Load()
        {
            DW.Framework.Data.DataContainer.ResetService();

            XmlConfig xml = new XmlConfig(path, file);
            xml.LoadXml();

            Elements = new ConfigSection();
           
            Elements.ConnectionString = xml.GetConnectionStrings();
          
            Elements.ProviderTypes = xml.GetProviderTypes();
        }
    }
}
