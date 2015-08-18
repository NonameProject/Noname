using Abitcareer.Business.Data_Providers_Contracts;
using Abitcareer.Business.Interfaces;
using Abitcareer.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SimpleCrypto;

namespace Abitcareer.Business.Components.Managers
{
    public class UserManager : BaseManager<User, IUserDataProvider>
    {
        public UserManager(ICacheManager manager, IUserDataProvider provider) : base(manager, provider) { } 

        protected override string Name
        {
            get
            {
                return "User";
            }
        }

        public void Create(User model)
        {
            ClearCache();

            var crypto = new SimpleCrypto.PBKDF2();

            model.PasswordSalt = crypto.GenerateSalt();

            model.Password = crypto.Compute(model.Password,model.PasswordSalt);

            provider.Create(model);
        }

        public IList<User> GetList()
        {
            return FromCache<IList<User>>(Thread.CurrentThread.CurrentUICulture.LCID + "_list", () =>
            {
                return provider.GetList();
            });
        }

        public User GetByEmail(string email)
        {
            return provider.GetByEmail(email);
        }

        public bool IsPasswordValid(string email, string password)
        {
            var user = provider.GetByEmail(email);
            if(user != null)
            {
                return String.Equals(user.Password, new PBKDF2().Compute(password, user.PasswordSalt));
            }
            return false;
        }

        public bool IsUserExists(string email)
        {
            return provider.GetByEmail(email) != null;

        }
    }
}
