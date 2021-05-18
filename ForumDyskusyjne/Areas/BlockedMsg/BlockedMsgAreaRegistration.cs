using System.Web.Mvc;

namespace ForumDyskusyjne.Areas.BlockedMsg
{
    public class BlockedMsgAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "BlockedMsg";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "BlockedMsg_default",
                "BlockedMsg/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}