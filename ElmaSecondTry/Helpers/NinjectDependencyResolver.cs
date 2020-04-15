using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using ElmaSecondTryBase.Repositories;
using ElmaSecondTryNHibernate.Repositories;

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
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IVacancyRepository>().To<VacancyRepository>();
            kernel.Bind<ICandidateRepository>().To<CandidateRepository>();
        }
    }
}