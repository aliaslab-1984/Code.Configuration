using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code.Configuration
{
    public class ConfigurationElementExt : System.Configuration.ConfigurationElement
    {
        protected new object this[string name]
        {
            get
            {
                if (base[name] is string)
                    return base[name].ToString().ApplyParsers();
                else
                    return base[name];
            }
            set { base[name] = value; }
        }
    }
}
