using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Code.Configuration
{
    public static class ValueParsers
    {
        private static Dictionary<Type, IValueParser> _parsers = new Dictionary<Type, IValueParser>();
                
        public static void AddParser(IValueParser parser)
        {
            if (!_parsers.ContainsKey(parser.GetType()))
                _parsers.Add(parser.GetType(), parser);
            else
                _parsers[parser.GetType()] = parser;
        }

        public static List<IValueParser> GetParsers()
        {
            return _parsers.Values.ToList();
        }
    }
}