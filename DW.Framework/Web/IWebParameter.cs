using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DW.Framework.Web
{
    public interface IWebParameter
    {
        // Properties
        string Name { get; }
        ParameterType ParameterType { get; }
    }

 

}
