using System.Web.Mvc;

namespace Pass.Manager.Web.Areas.Site
{
    public class SiteAreaRegistration : AreaRegistration
    {
        public const string UrlPrmPassSiteId = "passSiteId";
        public override string AreaName 
        {
            get 
            {
                return "Site";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Site_default",
                "Site/{" + UrlPrmPassSiteId + "}/{controller}/{action}/{id}",
                new { controller="PassProject", action = "Index", id = UrlParameter.Optional },
                new { passSiteId = @"\d+" },
                new[] { "Pass.Manager.Web.Areas.Site.Controllers" }
            );

        }
    }
}