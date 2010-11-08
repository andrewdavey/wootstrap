using System.Reflection;
using System.Web;
using Autofac;
using Autofac.Integration.Web;
using Autofac.Integration.Web.Mvc;

namespace Wootstrap.Infrastructure.Ioc
{
    /// <summary>
    /// IoC module that loads commonly used System.Web and System.Web.Mvc dependencies into the container.
    /// This includes application Controllers and Model Binders.
    /// </summary>
    public class SystemWebModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterHttpContext(builder);
            RegisterMvc(builder);
        }

        static void RegisterHttpContext(ContainerBuilder builder)
        {
            // Services that need the current HttpContext should avoid using HttpContext.Current directly.
            // It's hard to test. Instead, they can have the current context injected into their constructors.
            builder.Register(c => new HttpContextWrapper(HttpContext.Current)).As<HttpContextBase>().InstancePerHttpRequest();
            // For convenience, the following properties are also registered.
            builder.Register(c => c.Resolve<HttpContextBase>().Request).InstancePerHttpRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Response).InstancePerHttpRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Server).InstancePerHttpRequest();
            // Add/remove anything else required from HttpContext here.
        }

        static void RegisterMvc(ContainerBuilder builder)
        {
            var webApplicationAssembly = Assembly.GetExecutingAssembly();
            builder.RegisterControllers(webApplicationAssembly);
            builder.RegisterModelBinders(webApplicationAssembly);
        }
    }
}