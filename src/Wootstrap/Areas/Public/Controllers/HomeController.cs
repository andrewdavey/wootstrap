using System.Web.Mvc;
using Wootstrap.Controllers;

namespace Wootstrap.Areas.Public.Controllers
{
    public class HomeController : WootstrapController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}