using ElmaSecondTryBase.Entities;
using NHibernate;
using NHibernate.Cfg;
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
            return SessionFactory.OpenSession();
        }
    }
}