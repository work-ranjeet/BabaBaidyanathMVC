using System.Web.Mvc;

namespace Baidyanath.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AdminArea",
                "Admin/{controller}/{action}/{id}",
                new { controller= "AdminHome", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
