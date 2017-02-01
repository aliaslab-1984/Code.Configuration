using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Code.Configuration
{
    public static class ConfigurationExtensions
    {
        public static string ApplyParsers(this string ext)
        {
            IValueParser interpreter = ValueParsers.GetParsers().FirstOrDefault(p => p.CheckSyntax(ext));
            ext = Regex.Replace(ext, "^\\[(.*)\\]", "$1");
            if (interpreter == null)
                return ext;
            else
                return interpreter.Parse(ext).ApplyParsers();
        }
    }
}
