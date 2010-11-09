using System.Web;
using System.Web.Routing;

namespace Wootstrap.Infrastructure.Mvc
{
    /// <summary>
    /// Wraps an existing route, but ensures generated URLs have a lowercase path. 
    /// For example, "/Session/SignIn?ReturnUrl=/test" will be "/session/signin?ReturnUrl=/test".
    /// </summary>
    public class LowercaseRoute : RouteBase
    {
        public LowercaseRoute(RouteBase route)
        {
            this.route = route;
        }

        readonly RouteBase route;

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            var path = route.GetVirtualPath(requestContext, values);

            if (path != null)
            {
                LowercaseUrlPath(path);
            }

            return path;
        }

        void LowercaseUrlPath(VirtualPathData pathData)
        {
            var url = pathData.VirtualPath;
            var queryIndex = url.IndexOf('?');
            if (queryIndex < 0) queryIndex = url.Length;

            pathData.VirtualPath =
                url.Substring(0, queryIndex).ToLowerInvariant() +
                url.Substring(queryIndex);
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            return route.GetRouteData(httpContext);
        }
    }
}