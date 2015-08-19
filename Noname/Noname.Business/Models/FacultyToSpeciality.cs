using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.Business.Models
{
    public class FacultyToSpeciality : BaseModel
    {
        public virtual Faculty Faculty { get; set; }

        public virtual Speciality Speciality { get; set; }

        public virtual int Price { get; set; }
    }
}
