using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Code.Configuration
{
    public class JsonSettingSerializer : ISettingSerializer
    {
        public string Serialize(Dictionary<string, object> config)
        {
            return JsonConvert.SerializeObject(config);
        }

        public Dictionary<string, object> Deserialize(string config)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(config);
        }

        public bool IsValid(string config)
        {
            try
            {
                if (string.IsNullOrEmpty(config))
                    return false;
                else
                {
                    Deserialize(config);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}