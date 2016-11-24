using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Security.Application;

namespace DW.Framework.Security
{
    public sealed  class EncodeDecode
    {
        //public static string CrmHtmlDecode(string input)
        //{
        //    return HttpUtility.HtmlDecode(input);
        //}

        public static string HtmlEncode(string input)
        {
            return Microsoft.Security.Application.Encoder.HtmlEncode(input);
        }

        public static string JavaScriptEncode(string input)
        {
            return Microsoft.Security.Application.Encoder.JavaScriptEncode(input,false);
        }

        public static string HtmlAttributeEncode(string input)
        {
            return Microsoft.Security.Application.Encoder.HtmlAttributeEncode(input);
        }

        public static string CssEncode(string input)
        {
            return Microsoft.Security.Application.Encoder.CssEncode(input);
        }

        public static string HtmlFormUrlEncode(string input)
        {
            return Microsoft.Security.Application.Encoder.HtmlFormUrlEncode(input);
        }

        public static string UrlEncode(string input)
        {
            return Microsoft.Security.Application.Encoder.UrlEncode(input);
        }

        public static string XmlEncode(string input)
        {
            return Microsoft.Security.Application.Encoder.XmlEncode(input);
        }

        public static string XmlAttributeEncode(string input)
        {
            return Microsoft.Security.Application.Encoder.XmlAttributeEncode(input);
        }

        public static string SqlEncode(string input)
        {
            return input.Replace("'", "''").Replace("\\", "").Replace("--", "");
        }
    }
}
