using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Wootstrap.Helpers
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Returns the HTML for a basic editor form that will post back to the current action's URL.
        /// </summary>
        /// <param name="submitButtonText">Text to show on the form's submit button.</param>
        public static IHtmlString FormForModel<T>(this HtmlHelper<T> html, string submitButtonText = "Submit")
        {
            var action = GetCurrentActionUrl<T>(html);
            return new HtmlString(
                "<form method=\"post\" action=\"" + action + "\">" +
                html.EditorForModel().ToString() +
                "<div class=\"actions\"><button type=\"submit\">" + submitButtonText + "</button></div></form>"
            );
        }

        static string GetCurrentActionUrl<T>(HtmlHelper<T> html)
        {
            var url = new UrlHelper(html.ViewContext.RequestContext);
            return url.Action(html.ViewContext.RouteData.GetRequiredString("action"));
        }
    }
}