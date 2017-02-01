using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Code.Configuration
{
    public abstract class AbstractConfigValueFactory
    {
        public abstract AbstractConfigValue GetValue(string name);

        public virtual AbstractConfigValue GetValue(string config, ISettingSerializer serializer)
        {
            AbstractConfigValue value = null;

            Dictionary<string, object> tmp = serializer.Deserialize(config); 
            if (tmp.ContainsKey("ref"))
                return GetValue((string)tmp["ref"]);

            for (int i=0;i<tmp.Keys.Count;i++)
            {
                string prop=tmp.Keys.ElementAt(i);
                if (serializer.IsValid((string)tmp[prop]))
                {
                    tmp[prop] = GetValue((string)tmp[prop], serializer);
                }
            }

            Type type = Type.GetType((string)tmp["type"]);
            List<object> args = new List<object>();
            foreach (ParameterInfo item in type.GetConstructors().First().GetParameters())
            {
                if (tmp.ContainsKey(item.Name))
                    args.Add(tmp[item.Name]);
                else
                    args.Add(null);
            }

            value = (AbstractConfigValue)Activator.CreateInstance(type, args.ToArray());
            
            return value;
        }
    }
}