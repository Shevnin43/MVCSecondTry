using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using ElmaSecondTryNHibernate.Repositories;
using ElmaSecondTryNHibernate;
using NHibernate;
using AutoMapper;
using Ninject.Web.Common;
using System.Web;
using ElmaSecondTryBase.IRepositories;
using ElmaSecondTryBase.Entities;

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
            kernel.Bind<IMapper>().ToConstructor(c => new Mapper(mapperConfiguration)).InSingletonScope();
            kernel.Bind<ISessionFactory>().ToProvider<HibernateHelper>();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IAnnouncementRepository>().To<AnnouncementRepository>();

        }
    }
}