using System.Web;
using Autofac.Integration.Web;
using Wootstrap.Infrastructure.Initialization;

namespace Wootstrap
{
    public class MvcApplication : HttpApplication, IContainerProviderAccessor
    {
        protected void Application_Start()
        {
            _containerProvider = new ContainerProvider(Ioc.CreateApplicationContainer());
            Mvc.Initialize(_containerProvider);
        }

        /// <summary>Holds the application container.</summary>
        static IContainerProvider _containerProvider;

        /// <summary>Used by Autofac HttpModules to resolve and inject dependencies.</summary>
        public IContainerProvider ContainerProvider
        {
            get { return _containerProvider; }
        }
    }
}