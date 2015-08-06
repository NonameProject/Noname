using Abitcareer.Business.Data_Providers_Contracts;
using Abitcareer.Business.Interfaces;
using Abitcareer.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Abitcareer.Business.Components.Managers
{
    public class UserManager : BaseManager
    {
        IUserDataProvider provider;

        protected override string Name
        {
            get
            {
                return "User";
            }

            set
            {
            }
        }

        public UserManager(ICacheManager manager, IUserDataProvider provider)
            : base(manager)
        {
            this.provider = provider;
        }

        public void Create(User model)
        {
            ClearCache();
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
    }
}
