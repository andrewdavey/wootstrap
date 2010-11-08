using System.Web.Mvc;

namespace Wootstrap.Areas.Public
{
    public class PublicAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Public"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            // Moved default public route to Global.asax.cs to ensure it is registered
            // after all other areas.
        }
    }
}
