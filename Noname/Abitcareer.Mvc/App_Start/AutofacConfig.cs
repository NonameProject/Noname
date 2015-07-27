using Abitcareer.Business.Components.Managers;
using Abitcareer.Business.Data_Providers_Contracts;
using Abitcareer.Business.Interfaces;
using Abitcareer.NHibernateDataProvider.Data_Providers;
using Abitcareer.Web.Components;
using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Abitcareer.Mvc.App_Start
{
    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(AbitcareerApplication).Assembly);
            builder.RegisterType<UniversityManager>();
            builder.RegisterType<NHibernateUniversityDataProvider>()
                .As<IUniversityDataProvider>();
            builder.RegisterType<RuntimeCacheManager>()
                .As<ICacheManager>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
