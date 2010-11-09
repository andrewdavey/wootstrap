using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
            Response.Cookies.Add(CreateAuthCookie(model.Username, GetRoles(model.Username)));
            return Redirect("~");
        }

        string[] GetRoles(string username)
        {
            return new string[0];
        }

        HttpCookie CreateAuthCookie(string username, string[] roles)
        {
            var ticket = new FormsAuthenticationTicket(
                1,
                username,
                DateTime.Now,
                DateTime.Now.Add(FormsAuthentication.Timeout),
                false,
                string.Join(",", roles),
                FormsAuthentication.FormsCookiePath
            );
            return new HttpCookie(FormsAuthentication.FormsCookieName)
            {
                HttpOnly = true,
                Value = FormsAuthentication.Encrypt(ticket)
            };
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("~");
        }
    }
}
