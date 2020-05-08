using ElmaSecondTryNHibernate.NHibernateMappings;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using Ninject.Activation;

namespace ElmaSecondTryNHibernate
{
    public class HibernateHelper : Provider<ISessionFactory>
    {
        /// <summary>
        /// Настройка и конфигурирование NHibernate
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected override ISessionFactory CreateInstance(IContext context)
        {
            var mapper = new ModelMapper();
            mapper.AddMappings(typeof(HibernateModule).Assembly.ExportedTypes);
            var mappings = mapper.CompileMappingForAllExplicitlyAddedEntities();
            var cfg = new Configuration();
            cfg.DataBaseIntegration(c =>
            {
                c.Dialect<MsSql2012Dialect>();
                c.ConnectionString = "Initial Catalog=RecruitmentAgency;Integrated Security=True;Pooling=False";
            });
            cfg.AddMapping(mappings);
            return cfg.BuildSessionFactory();
        }
    }
}