using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Abitcareer.Business.Components.Extensions
{
    public static class DictonaryExtensions
    {
        public static string ToXmlString<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            return new XElement("fields", dictionary
                        .Select(x => new XElement("field", new XAttribute("key", x.Key), new XAttribute("value", x.Value))))
                        .ToString();
        }
    }
}
