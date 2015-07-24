
using System.Collections.Generic;
namespace Abitcareer.Business.Models
{
    public class Region : BaseModel
    {
        public virtual IList<City> Cities { get; set; }
    }
}
