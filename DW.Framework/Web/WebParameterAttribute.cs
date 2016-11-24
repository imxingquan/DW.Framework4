using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace DW.Framework.Web
{
    public abstract class WebParameterAttribute : Attribute, IWebParameter
    {
        // Fields
        private string _name;
        private ParameterType _parameterType;

        // Methods
        protected WebParameterAttribute(string name, ParameterType parameterType)
        {
            if (name != null)
            {
                this._name = name.ToUpper(CultureInfo.InvariantCulture);
            }
            this._parameterType = parameterType;
        }

        // Properties
        public string Name
        {
            get
            {
                return this._name;
            }
        }

        public ParameterType ParameterType
        {
            get
            {
                return this._parameterType;
            }
        }
    }


}
