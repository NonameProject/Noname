using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.Business.Models
{
    public class City
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string NameEN { get; set; }

        public virtual int RegionId { get; set; }
    }
}
