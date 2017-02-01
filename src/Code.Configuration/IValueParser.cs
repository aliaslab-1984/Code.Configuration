using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code.Configuration
{
    public interface IValueParser
    {
        bool CheckSyntax(string value);

        string Parse(string value);
    }
}
