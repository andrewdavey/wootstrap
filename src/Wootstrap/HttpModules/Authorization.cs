using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace Wootstrap.HttpModules
{
    /// <summary>
    /// After a request has been authenticated, this module will extract the user's authorized roles 
    /// from the data stored in the user's authentication cookie. The request's User property is
    /// updated to be a principal with the roles added.
    /// </summary>
    public class Authorization : IHttpModule
    {
        public void Init(HttpApplication app)
        {
            app.AuthenticateRequest += AuthenticateRequest;
        }

        void AuthenticateRequest(object sender, EventArgs e)
        {
            var formsIdentity = TryGetFormsIdentityWithUserData();
            if (formsIdentity == null) return;

            HttpContext.Current.User = CreatePrincipalWithRoles(formsIdentity);
        }

        FormsIdentity TryGetFormsIdentityWithUserData()
        {
            var user = HttpContext.Current.User;
            if (user == null) return null;
            
            var formsIdentity = user.Identity as FormsIdentity;
            return IsValidFormsIdentity(formsIdentity) ? formsIdentity : null;
        }

        bool IsValidFormsIdentity(FormsIdentity formsIdentity)
        {
            return formsIdentity != null
                && formsIdentity.IsAuthenticated
                && !string.IsNullOrEmpty(formsIdentity.Ticket.UserData);
        }

        GenericPrincipal CreatePrincipalWithRoles(FormsIdentity formsIdentity)
        {
            var roles = formsIdentity.Ticket.UserData.Split(',');
            return new GenericPrincipal(formsIdentity, roles);
        }

        public void Dispose() { }
    }
}