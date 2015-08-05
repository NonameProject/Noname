using System.Collections.Generic;

namespace Abitcareer.Business.Models
{
    public class Speciality : BaseModel
    {
        public virtual int DirectionCode { get; set; }

        public virtual int Code { get; set; }

        //public virtual IList<Faculty> Faculties { get; set; }

        //public Speciality()
        //{
        //    Faculties = new List<Faculty>();
        //}
    }
}
