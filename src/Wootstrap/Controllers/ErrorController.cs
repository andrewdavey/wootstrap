using System.Web.Mvc;

namespace Wootstrap.Controllers
{
    public class ErrorController : WootstrapController
    {
        public ActionResult NotFound(string url)
        {
            // Copied from http://stackoverflow.com/questions/619895/how-can-i-properly-handle-404s-in-asp-net-mvc/2577095#2577095

            // If the url is relative ('NotFound' route) then replace with Requested path
            ViewModel.RequestedUrl = Request.Url.OriginalString.Contains(url) && Request.Url.OriginalString != url ?
                Request.Url.OriginalString : url;

            // Dont get the user stuck in a 'retry loop' by
            // allowing the Referrer to be the same as the Request
            ViewModel.ReferrerUrl = Request.UrlReferrer != null &&
                Request.UrlReferrer.OriginalString != ViewModel.RequestedUrl ?
                Request.UrlReferrer.OriginalString : null;

            Response.StatusCode = 404;
            return View();
        }
    }
}