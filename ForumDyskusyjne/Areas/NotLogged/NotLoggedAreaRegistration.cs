using System.Web.Mvc;

namespace ForumDyskusyjne.Areas.NotLogged
{
    public class NotLoggedAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "NotLogged";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "NotLogged_default",
                "NotLogged/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}