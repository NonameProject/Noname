using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Abitcareer.Business.Models
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    sealed class LocalizableFieldAttribute : Attribute
    {
    }

    public class BaseModel
    {
        public virtual int Id { get; set; }

        [LocalizableField]
        public virtual string Name { get; set; }

        public virtual Dictionary<string, object> Fields { get; set; }

        public virtual string Xml
        {
            get
            {
                return new XElement(
                     "fields",
                     Fields.Select(x => new XElement("field", new XAttribute("key", x.Key), new XAttribute("value", x.Value)))).ToString();
            }
            set
            {
                XElement xElem2 = XElement.Parse(value);
                Fields = xElem2.Descendants("item")
                                    .ToDictionary(x => (string)x.Attribute("key"), x => (object)x.Attribute("value"));
            }
        }
    }
}
