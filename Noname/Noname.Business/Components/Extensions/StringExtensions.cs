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
            var tempDict = XElement
                    .Parse(str)
                    .Descendants("field")
                    .ToDictionary(x => x.Attribute("key").ToString(), x => x.Attribute("value").ToString());
            var result = new Dictionary<TKey, TValue>();
            foreach (var item in tempDict)
	        {
                result[(TKey)Convert.ChangeType(item.Key, typeof(TKey))] = (TValue)Convert.ChangeType(item.Value, typeof(TValue));		 
	        }
            return result;
        }
    }
}
