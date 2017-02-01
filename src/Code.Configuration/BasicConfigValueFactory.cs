using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Code.Configuration
{
    public class BasicConfigValueFactory : AbstractConfigValueFactory
    {
        protected object _lk = new object();
        protected Dictionary<string, AbstractConfigValue> _store = new Dictionary<string, AbstractConfigValue>();
        
        public override AbstractConfigValue GetValue(string name)
        {
            lock (_lk)
            {
                return _store[name];
            }
        }

        public override AbstractConfigValue GetValue(string config, ISettingSerializer serializer)
        {
            lock (_lk)
            {
                AbstractConfigValue value = null;
                Dictionary<string, object> tmp = serializer.Deserialize(config);
                if (_store.ContainsKey((string)tmp["name"]))
                {
                    value = GetValue((string)tmp["name"]);
                }
                else
                {
                    value = base.GetValue(config, serializer);
                    _store.Add((string)tmp["name"], value);
                }

                return value;
            }
        }
    }
}