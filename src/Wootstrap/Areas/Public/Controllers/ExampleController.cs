using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wootstrap.Areas.Public.ViewModels;

namespace Wootstrap.Areas.Public.Controllers
{
    public class ExampleController : Controller
    {
        public ActionResult Index()
        {
            return View(new ExampleForm());
        }

    }
}
