using System.Web.Mvc;
using Wootstrap.Filters;

namespace Wootstrap.Infrastructure.Mvc
{
    public class Filtering
    {
        public static void Initialize(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorFilter());
        }
    }
}