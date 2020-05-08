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
    public class HibernateHelper : Provider<ISessionFactory>
    {
        
        protected override ISessionFactory CreateInstance(IContext context)
        {
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


            //new SchemaUpdate(cfg).Execute (true, true);
            return cfg.BuildSessionFactory();
        }
    }
}