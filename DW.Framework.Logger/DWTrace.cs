using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using DW.Framework.Logger.BLL;

namespace DW.Framework.Logger
{
    public class DWTrace
    {
        public static void Write(string message, string category)
        {
            LogsAction.Call.Insert(0, category, message, DateTime.Now);
            //Trace.Write(string.Format("{0}:{1}", DateTime.Now, message), category);
        }

        public static void Write(string message)
        {
            Write(message, string.Empty);
        }
    }
}
