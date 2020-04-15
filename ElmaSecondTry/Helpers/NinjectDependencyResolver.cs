using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using ElmaSecondTryBase.Repositories;
using ElmaSecondTryNHibernate.Repositories;
using ElmaSecondTryNHibernate;
using NHibernate;

namespace ElmaSecondTry.Helpers
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IEntityRepository>().To<EntityRepository>();
            //kernel.Bind<ISession>().To<HibernateHelper>();
        }
    }
}