using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.Business.Models
{
    public class FacultiesToSpecialities : BaseModel
    {
        public virtual string FacultyId { get; set; }

        public virtual string SpecialityId { get; set; }
    }
}
