using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code.Configuration
{
    public interface ISettingSerializer
    {
        string Serialize(Dictionary<string,object> config);

        Dictionary<string, object> Deserialize(string config);

        bool IsValid(string config);
    }
}
