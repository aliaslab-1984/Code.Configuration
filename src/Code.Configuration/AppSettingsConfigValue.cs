using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Code.Configuration
{
    public class AppSettingsConfigValue : AbstractConfigValue
    {
        protected string _settingName;
        protected bool _hasDefaultValue;
        protected object _defaultValue;

        public AppSettingsConfigValue(object name, object defaultValue)
        {
            if (string.IsNullOrEmpty(name is AbstractConfigValue ? (string)(AbstractConfigValue)name : (string)name))
                throw new ArgumentException("name must be set.");

            _settingName = name is AbstractConfigValue?(string)(AbstractConfigValue)name:(string)name;
            _hasDefaultValue = defaultValue!=null;
            _defaultValue = _hasDefaultValue ? defaultValue : null;
        }
        
        protected override object Value
        {
            get
            {
                if (!ConfigurationManager.AppSettings.AllKeys.Contains(_settingName))
                    if (!_hasDefaultValue)
                        throw new Exception(string.Format("Configuration parameter missing: {0}", _settingName));
                    else
                        return ParseValue(_defaultValue is AbstractConfigValue?(string)(AbstractConfigValue)_defaultValue:(string)_defaultValue);
                else
                    return ParseValue(ConfigurationManager.AppSettings[_settingName]);
            }
        }

        protected override Dictionary<string, object> GetProps()
        {
            return new Dictionary<string, object>() { { "type", GetType() }, { "name", _settingName }, { "defaultValue", _defaultValue } };
        }
    }
}
