using System.Collections.Generic;

namespace Abitcareer.Business.Models
{
    public class City : BaseModel
    {
        public virtual int RegionId { get; set; }

        public virtual Region Region { get; set; }

        public virtual IList<University> Universities { get; set; }
    }
}
