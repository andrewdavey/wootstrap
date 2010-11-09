using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wootstrap.Areas.Public.ViewModels;
using Wootstrap.Controllers;

namespace Wootstrap.Areas.Public.Controllers
{
    public class SessionController : WootstrapController
    {
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(SignInViewModel model)
        {
            if (!ModelState.IsValid) return View();
            return Redirect("~");
        }
    }
}
