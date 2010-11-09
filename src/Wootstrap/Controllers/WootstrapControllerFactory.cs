using System;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac.Integration.Web;
using Autofac.Integration.Web.Mvc;

namespace Wootstrap.Controllers
{
    /// <summary>
    /// Adds better "controller not found" error handling by returning the ErrorController instead of 
    /// throwing an exception.
    /// </summary>
    public class WootstrapControllerFactory : AutofacControllerFactory
    {
        public WootstrapControllerFactory(IContainerProvider containerProvider)
            : base(containerProvider)
        {
        }

        protected override IController GetControllerInstance(RequestContext context, Type controllerType)
        {
            if (controllerType != null)
            {
                return base.GetControllerInstance(context, controllerType);
            }
            else
            {
                ChangeToErrorController(context);
                return base.GetControllerInstance(context, typeof(ErrorController));
            }
        }

        void ChangeToErrorController(RequestContext context)
        {
            context.RouteData.Values["controller"] = "Error";
            context.RouteData.Values["action"] = "NotFound";
            context.RouteData.Values["url"] = context.HttpContext.Request.Url.OriginalString;
        }
    }
}