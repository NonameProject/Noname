using Abitcareer.Business.Models;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using StackExchange.Profiling;
using StackExchange.Profiling.NHibernate;

namespace Events.NHibernateDataProvider.NHibernateCore
{
    public class Helper
    {
        private static ISessionFactory sessionFactory;

        public static ISession OpenSession()
        {
            if (sessionFactory == null)
            {
                InitializeSessionFactory();
            }

            return sessionFactory.OpenSession();
        }

        private static void InitializeSessionFactory()
        {
            sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                              .Driver(typeof(StackExchange.Profiling.NHibernate.Drivers.MiniProfilerSql2008ClientDriver).AssemblyQualifiedName)
                              .ConnectionString(
                                  @"Server=tcp:ead9qcxrdo.database.windows.net,1433;Database=abitcareer;User ID=abitcareer@ead9qcxrdo;Password=ISMabit3;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;")
                              .ShowSql()
                )
                .Mappings(m =>
                          m.FluentMappings
                              .AddFromAssemblyOf<Helper>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg)
                                                .Create(true, true))
                .BuildSessionFactory();
        }
    }
}
