
using System.Collections.Generic;
namespace Abitcareer.Business.Models
{
    public class Faculty : BaseModel
    {
        public virtual University University { get; set; }

        public virtual IList<Speciality> Specialities { get; set; }
    }
}
