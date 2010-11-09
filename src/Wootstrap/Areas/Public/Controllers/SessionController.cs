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
        public ActionResult SignIn(string returnUrl)
        {
            returnUrl = GetSafeReturnUrl(returnUrl);
            ModelState.Clear(); // otherwise we get the ModelState's original ReturnUrl in the view!
            return View(new SignInViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public ActionResult SignIn(SignInViewModel model)
        {
            // NOTE: The SignInViewModel validation will authenticate the username and password.
            // This action is only called when model is valid, thanks to RequireValidModelStateFilter.

            AddAuthCookieToResponse(model);
            return Redirect(GetSafeReturnUrl(model.ReturnUrl));
        }

        string GetSafeReturnUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return FormsAuthentication.DefaultUrl;
            }
            else
            {
                return returnUrl;
            }
        }

        void AddAuthCookieToResponse(SignInViewModel model)
        {
            Response.Cookies.Add(
                CreateAuthCookie(
                    model.Username, 
                    GetRoles(model.Username)
                )
            );
        }

        string[] GetRoles(string username)
        {
            // TODO: Get the roles for the username.
            return new[] { "admin" };
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
