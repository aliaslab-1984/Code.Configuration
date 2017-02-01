using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Code.Configuration
{
    public abstract class AbstractConfigValue
    {
        #region implicit casts

        public static implicit operator bool(AbstractConfigValue val)
        {
            return Convert.ToBoolean(val.Value);
        }

        public static implicit operator int(AbstractConfigValue val)
        {
            return Convert.ToInt32(val.Value);
        }

        public static implicit operator long(AbstractConfigValue val)
        {
            return Convert.ToInt64(val.Value);
        }

        public static implicit operator double(AbstractConfigValue val)
        {
            return Convert.ToDouble(val.Value);
        }

        public static implicit operator string(AbstractConfigValue val)
        {
            return val.Value.ToString();
        }

        #endregion

        protected virtual string ParseValue(string value)
        {
            return value.ApplyParsers();
        }
        
        protected abstract object Value { get; }

        protected abstract Dictionary<string, object> GetProps();

        public virtual string Serialize(ISettingSerializer serializer)
        {
            return serializer.Serialize(GetProps());
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
