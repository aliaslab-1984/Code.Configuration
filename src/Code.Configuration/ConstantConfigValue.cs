using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Code.Configuration
{
    public class ConstantConfigValue : AbstractConfigValue
    {
        protected string _name;
        protected object _value;

        public ConstantConfigValue(object name,object value)
        {
            if (string.IsNullOrEmpty(name is AbstractConfigValue ? (string)(AbstractConfigValue)name : (string)name))
                throw new ArgumentException("name must be set.");

            _name = name is AbstractConfigValue ? (string)(AbstractConfigValue)name : (string)name;
            _value = value;
        }

        protected override object Value
        {
            get
            {
                return ParseValue((string)_value);
            }
        }

        protected override Dictionary<string, object> GetProps()
        {
            return new Dictionary<string, object>() { { "type", GetType() }, { "name", _name }, { "value", _value } };
        }
    }
}