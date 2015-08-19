using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleCrypto;

namespace Abitcareer.Business.Models
{
    public class User : BaseModel
    {
        public virtual string Email { get; set; }

        public virtual string PasswordSalt { get; set; }

        public virtual string Password { get; set; }

        public User()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
