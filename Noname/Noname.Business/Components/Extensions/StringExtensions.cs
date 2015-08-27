using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Abitcareer.Business.Components.Extensions
{
    public static class StringExtensions
    {
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this string str)
        {
            var result = new Dictionary<TKey, TValue>();
            if(string.IsNullOrEmpty(str))
                return result;
            var tempDict = XElement
                        .Parse(str)
                        .Descendants("field")
                        .ToDictionary(x => x.Attribute("key").Value, x => x.Attribute("value").Value);
            var type = typeof(TValue);
            if (type == ( typeof(Nullable<int>) ))
                type = typeof(int);
            foreach (var item in tempDict)
                result[(TKey)Convert.ChangeType(item.Key, typeof(TKey))] = (TValue) (string.IsNullOrEmpty(item.Value) ? null : Convert.ChangeType(item.Value, type));
            return result;
        }
    }
}
