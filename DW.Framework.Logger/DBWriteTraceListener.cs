using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using DW.Framework.Logger.BLL;

namespace DW.Framework.Logger
{
    public class DBWriteTraceListener : TraceListener
    {

        //public override void Write(string message)
        //{
        //    Write(message, String.Empty);
        //}

        //public override void WriteLine(string message)
        //{
        //    Write(message, String.Empty);
        //}

        //public override void Write(string message,string category )
        //{
        //    LogsAction.Call.Insert(0, category, message, DateTime.Now);
        //}

        public override void Write(string message)
        {
            Write(message, string.Empty);
        }

        public override void Write(string message, string category)
        {
            base.Write(message, category);
            LogsAction.Call.Insert(0, category, message, DateTime.Now);
        }

        public override void WriteLine(string message)
        {
            throw new NotImplementedException();
        }
    }
}
