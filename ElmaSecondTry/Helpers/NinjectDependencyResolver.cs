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

namespace ElmaSecondTry.Helpers
{
    /// <summary>
    /// Класс настройки обеспечивающий работу DependencyInjection через Ninject
    /// </summary>
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;
        /// <summary>
        /// Конструктор с получением реализации интерфейса IKernel
        /// </summary>
        /// <param name="kernelParam"></param>
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;
            AddBindings();
        }
        /// <summary>
        /// Метод получения сервиса из "кучи" сервисов 
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }
        /// <summary>
        /// Методо получения всех сервисов из кучи
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
        /// <summary>
        /// Собственно Binding - сопоставление интерфейсов и их реализации с включением их в "кучу"
        /// </summary>
        private void AddBindings()
        {
            var mapperConfiguration = Mappings.ConfigureMapping();
            _kernel.Bind<IMapper>().ToConstructor(c => new Mapper(mapperConfiguration)).InSingletonScope();
            _kernel.Bind<ISessionFactory>().ToProvider<HibernateHelper>();
            _kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            _kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            _kernel.Bind<IUserRepository>().To<UserRepository>();
            _kernel.Bind<IAnnouncementRepository>().To<AnnouncementRepository>();

        }
    }
}