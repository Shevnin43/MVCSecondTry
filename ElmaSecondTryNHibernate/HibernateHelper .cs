using ElmaSecondTryBase.Entities;
using ElmaSecondTryNHibernate.NHibernateMappings;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using Ninject.Activation;

namespace ElmaSecondTryNHibernate
{
    public class HibernateHelper : Provider<ISession>
    {

        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var cfg = new Configuration();
                    cfg.Configure();
                    cfg.AddAssembly(typeof(UserBase).Assembly);
                    new SchemaExport(cfg).Execute(true, true, false);
                    _sessionFactory = cfg.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }
        
        protected override ISession CreateInstance(IContext context)
        {
            /*var cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly(typeof(HibernateModule).Assembly);*/

            var mapper = new ModelMapper();
            mapper.AddMappings(typeof(HibernateModule).Assembly.ExportedTypes);
            var mappings = mapper.CompileMappingForAllExplicitlyAddedEntities();
            var cfg = new Configuration();
            cfg.DataBaseIntegration(c =>
            {
                c.Dialect<MsSql2012Dialect>();
                c.ConnectionString = "Data Source=WIN-POTS66R4BQ3;Initial Catalog=RecruitmentAgency;Integrated Security=True;Pooling=False";
            });
            cfg.AddMapping(mappings);


            //new SchemaExport(cfg).Execute(true, true, false);
            var sessionFactory = cfg.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}