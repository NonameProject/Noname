using Abitcareer.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.Business.Components.XmlServices.Entities
{
    public class NodeModel
    {
        public Region Region { get; set; }

        public City City { get; set; }

        public University University { get; set; }

        public Faculty Faculty { get; set; }

        public Speciality Speciality { get; set; }

        public FacultiesToSpecialities FacultyToSpeciality { get; set; }
        
        public NodeModel()
        {
            Region = new Region();

            City = new City();

            University = new University();

            Faculty = new Faculty();

            Speciality = new Speciality();

            FacultyToSpeciality = new FacultiesToSpecialities();
        }
    }
}
