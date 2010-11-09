using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wootstrap.Filters
{
    /// <summary>
    /// Catches when a controller's ModelState is not valid. Returns the view again instead allowing the action to run.
    /// This filter can be disable per controller or action by adding [DisableRequireValidModelStateFilter].
    /// </summary>
    public class RequireValidModelStateFilter : FilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (IsDisabled(filterContext)) return;
            
            var controller = filterContext.Controller as Controller;
            if (InvalidModelState(filterContext, controller))
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = filterContext.ActionDescriptor.ActionName,
                    ViewData = controller.ViewData,
                    TempData = controller.TempData
                };
            }
        }

        bool IsDisabled(ActionExecutingContext filterContext)
        {
            return filterContext.ActionDescriptor.GetCustomAttributes(typeof(DisableRequireValidModelStateFilterAttribute), false).Any()
                || filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(DisableRequireValidModelStateFilterAttribute), false).Any();
        }

        bool IsPost(ActionExecutingContext filterContext)
        {
            return filterContext.HttpContext.Request.HttpMethod.Equals("post", StringComparison.OrdinalIgnoreCase);
        }

        bool InvalidModelState(ActionExecutingContext filterContext, Controller controller)
        {
            return IsPost(filterContext)
                && controller != null
                && !controller.ModelState.IsValid;
        }
        
        public void OnActionExecuted(ActionExecutedContext filterContext) { }
    }

    public class DisableRequireValidModelStateFilterAttribute : Attribute { }
}