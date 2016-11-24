using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Text.RegularExpressions;

namespace DW.Framework.SMS
{
    /// <summary>
    /// 短信发送模块
    /// 1.需要在配置文件中配置账户
    /// <appSettings>
    ///    <add key="smsuser" value="13394893721"/>
    ///    <add key="smspwd" value="27278617"/>
    /// </summary>
    /// 2. 增加配置节
    /*
    <system.serviceModel>
        <bindings>
            <basicHttpBinding>
                <binding name="ServicesSoap" closeTimeout="00:01:00" openTimeout="00:01:00"
                    receiveTimeout="00:10:00" sendTimeout="00:01:00" allowCookies="false"
                    bypassProxyOnLocal="false" hostNameComparisonMode="StrongWildcard"
                    maxBufferSize="65536" maxBufferPoolSize="524288" maxReceivedMessageSize="65536"
                    messageEncoding="Text" textEncoding="utf-8" transferMode="Buffered"
                    useDefaultWebProxy="true">
                    <readerQuotas maxDepth="32" maxStringContentLength="8192" maxArrayLength="16384"
                        maxBytesPerRead="4096" maxNameTableCharCount="16384" />
                    <security mode="None">
                        <transport clientCredentialType="None" proxyCredentialType="None"
                            realm="" />
                        <message clientCredentialType="UserName" algorithmSuite="Default" />
                    </security>
                </binding>
            </basicHttpBinding>
        </bindings>
        <client>
            <endpoint address="http://18dx.cn/api/services.asmx" binding="basicHttpBinding"
                bindingConfiguration="ServicesSoap" contract="SmsAPI.ServicesSoap"
                name="ServicesSoap" />
        </client>
    </system.serviceModel>
     */
    /// 
    public class Sms
    {
        public static string smsuser = ConfigurationManager.AppSettings["smsuser"];
        public static string smspwd = ConfigurationManager.AppSettings["smspwd"];

        public static string Send(string mobile, string smsmsg)
        {
            SmsAPI.ServicesSoapClient sms = new SmsAPI.ServicesSoapClient();

            string hash = GetMD5String(smsuser + GetMD5String(smspwd) + 0 + mobile + smsmsg.Replace("\r\n", "\n") + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            SmsAPI.ArrayOfString a_mobile = new SmsAPI.ArrayOfString();
            SmsAPI.ArrayOfString a_smsmsg = new SmsAPI.ArrayOfString();
            a_mobile.Add(mobile);
            a_smsmsg.Add(smsmsg);
            string errmsg = sms.SendSms(smsuser, 0, a_mobile, a_smsmsg, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), hash);
            string val = GetQueryStringValue(errmsg, "errid");
            if (val == "1")
            {
                return "1";
            }
            else
            {
               return GetQueryStringValue(errmsg, "msg");
            }

        }

        /// <summary>
        /// 提取值，如source: "xxx=yyy&t1=zzz", 则GetQueryStringValue(source,"xxx")的返回值为"yyy"
        /// </summary>
        public static string GetQueryStringValue(string source, string name)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            Regex reg = new Regex(string.Format(@"(^|&){0}=(?<value>(.*?))(&|$)", name), RegexOptions.IgnoreCase);
            MatchCollection mc = reg.Matches(source);
            if (mc.Count > 0)
            {
                return mc[0].Result("${value}");
            }
            return string.Empty;
        }
        /// <summary>
        /// MD5加密,返回MD5加密后的32位大写字符串
        /// </summary>
        public static string GetMD5String(string source)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(source, "MD5");
        }
    }
}
