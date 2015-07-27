using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.Business.Models
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    sealed class LocalizebleFieldAttribute : Attribute
    {        
    }

    public class BaseModel
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string NameEN { get; set; }

        //[LocalizebleField]
        //public string Title { get; set; }

        //public Dictionary<string, object> Fields { get; set; }

        //public string Xml
        //{
        //    get
        //    {
        //        return "";
        //        //Serialize fields
        //        //Title_1033
        //    }
        //    set
        //    {
        //        //Fields = deserialize value;
        //    }
        //}
    }
}
