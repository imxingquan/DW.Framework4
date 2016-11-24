using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DW.Framework.Web
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class QueryStringParameterAttribute : WebParameterAttribute
    {
        // Methods
        public QueryStringParameterAttribute(string name, ParameterType parameterType)
            : base(name, parameterType)
        {
        }
    }


}
