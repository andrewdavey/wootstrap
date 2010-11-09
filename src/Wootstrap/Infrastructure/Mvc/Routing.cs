using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web;

namespace Wootstrap.Infrastructure.Mvc
{
    public static class Routing
    {
        public static void Initialize(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // The Public area has no path prefix in the URL, so it must be registered
            // after all the other Areas. Otherwise it may capture their URLs.
            MapPublicAreaRoute(routes);

            ConvertRoutesToLowercase(routes);
        }

        static void MapPublicAreaRoute(RouteCollection routes)
        {
            var publicRoute = routes.MapRoute(
                "Public_default",
                "{controller}/{action}/{id}",
                new { controller = "home", action = "Index", id = UrlParameter.Optional }
            );
            publicRoute.DataTokens["Area"] = "Public";

            routes.MapRoute("NotFound", "{*url}", new { controller = "Error", action = "NotFound" });
        }

        static void ConvertRoutesToLowercase(RouteCollection routes)
        {
            var lowercaseRoutes = routes.Select(r => new LowercaseRoute(r)).ToArray();
            routes.Clear();
            foreach (var route in lowercaseRoutes)
            {
                routes.Add(route);
            }
        }
    }
}