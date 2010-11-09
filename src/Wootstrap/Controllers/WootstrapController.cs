using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Wootstrap.Controllers
{
    /// <summary>
    /// Base type for all controllers in this web application.
    /// </summary>
    public abstract class WootstrapController : Controller
    {
        protected override void HandleUnknownAction(string actionName)
        {
            // If controller is ErrorController dont 'nest' exceptions
            if (this.GetType() != typeof(ErrorController))
                this.InvokeHttp404();
        }

        void InvokeHttp404()
        {
            var errorRoute = new RouteData();
            errorRoute.Values.Add("controller", "Error");
            errorRoute.Values.Add("action", "NotFound");
            errorRoute.Values.Add("url", Request.Url.OriginalString);
            var context = new RequestContext(HttpContext, errorRoute);

            var factory = ControllerBuilder.Current.GetControllerFactory();
            var errorController = (IController)factory.CreateController(context, "Error");
            errorController.Execute(context);
        }
    }
}
