using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using ElmaSecondTryBase.Repositories;
using ElmaSecondTryNHibernate.Repositories;
using ElmaSecondTryNHibernate;
using NHibernate;
using AutoMapper;
using Ninject.Web.Common;
using System.Web;

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
            var mapperConfiguration = Mappings.ConfigureMapping();
            kernel.Bind<ISession>().ToProvider<HibernateHelper>();
            kernel.Bind<IMapper>().ToConstructor(c => new Mapper(mapperConfiguration)).InSingletonScope();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            kernel.Bind<IEntityRepository>().To<EntityRepository>();
        }
    }
}