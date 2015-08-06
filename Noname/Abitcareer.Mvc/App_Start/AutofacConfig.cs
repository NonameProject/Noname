using Abitcareer.Business.Components.Managers;
using Abitcareer.Business.Data_Providers_Contracts;
using Abitcareer.Business.Interfaces;
using Abitcareer.NHibernateDataProvider.Data_Providers;
using Abitcareer.Web.Components;
using Autofac;
using Autofac.Integration.Mvc;
using Events.Business.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using team2project.Components;

namespace Abitcareer.Mvc.App_Start
{
    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(AbitcareerApplication).Assembly);

            builder.RegisterType<WebUserContext>().As<UserContext>().InstancePerHttpRequest();

            builder.RegisterType<UniversityManager>();

            builder.RegisterType<RegionManager>();

            builder.RegisterType<CityManager>();

            builder.RegisterType<FacultyManager>();

            builder.RegisterType<SpecialityManager>();

            builder.RegisterType<UserManager>();

            builder.RegisterType<NHibernateRegionDataProvider>()
                .As<IRegionDataProvider>();

            builder.RegisterType<NHibernateCityDataProvider>()
                .As<ICityDataProvider>();

            builder.RegisterType<NHibernateFacultyDataProvider>()
                .As<IFacultyDataProvider>();

            builder.RegisterType<NHibernateSpecialityDataProvider>()
                .As<ISpecialityDataProvider>();
            
            builder.RegisterType<NHibernateUniversityDataProvider>()
                .As<IUniversityDataProvider>();

            builder.RegisterType<NHibernateUserDataProvider>()
                .As<IUserDataProvider>();

            builder.RegisterType<RuntimeCacheManager>()
                .As<ICacheManager>();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
