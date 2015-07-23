using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.Business.Models
{
    public class University
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NameEN { get; set; }

        public int CityId { get; set; }

        public int Rating { get; set; }

        public string Link { get; set; }
    }
}
