using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;
using Abitcareer.Business.Components.Extensions;
using Lucene.Net.Documents;
using Abitcareer.Business.Components.Lucene.Attributes;

namespace Abitcareer.Business.Models
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    sealed class LocalizableFieldAttribute : Attribute
    {
    }

    public class BaseModel
    {
        [Storable(Type = Field.Index.NOT_ANALYZED)]
        public virtual string Id { get; set; }

        [LocalizableField]
        [Storable(Type=Field.Index.ANALYZED)]
        public virtual string Name { get; set; }

        public virtual Dictionary<string, string> Fields { get; set; }

        public virtual string Xml
        {
            get
            {
                return Fields.ToXmlString<string, string>();
            }
            set
            {
                Fields = value.ToDictionary<string, string>();
            }
        }

        public BaseModel()
        {
            Id = Guid.NewGuid().ToString();
            Fields = new Dictionary<string, string>();
        }
    }
}
