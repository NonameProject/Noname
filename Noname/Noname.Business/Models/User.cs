using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.Business.Models
{
    public class User
    {
        public virtual string Id { get; set; }

        public virtual string Email { get; set; }

        public virtual string Password { get; set; }
    }
}
