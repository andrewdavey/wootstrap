using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac.Integration.Web;

namespace Wootstrap.Infrastructure
{
    public class WootstrapApplication : HttpApplication
    {
        protected void Application_Start()
        {
            _containerProvider = new ContainerProvider(Ioc.Wootstrapper.CreateApplicationContainer());
            Mvc.Wootstrapper.Initialize(_containerProvider);
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