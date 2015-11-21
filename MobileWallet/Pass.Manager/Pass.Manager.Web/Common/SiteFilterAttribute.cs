using System.Web.Mvc;
using Common.Web;
using Microsoft.Practices.Unity;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Areas.Site;

namespace Pass.Manager.Web.Common
{
    public class SiteFilterAttribute : ActionFilterAttribute
    {
        [Dependency]
        public IPassSiteService PassSiteService { get; set; }

        [Dependency]
        public UserContext<UserContextData> AuthUserContext { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            int siteId;
            var strSiteId = filterContext.RouteData.GetRouteValue<string>(SiteAreaRegistration.UrlPrmPassSiteId);
            if (!int.TryParse(strSiteId, out siteId))
            {
                AuthUserContext.UserData.SiteId = null;
                AuthUserContext.UserData.SiteName = null;
            }
            else if (siteId != AuthUserContext.UserData.SiteId)
            {
                PassSite site = PassSiteService.Get(siteId);
                AuthUserContext.UserData.SiteId = site.PassSiteId;
                AuthUserContext.UserData.SiteName = site.Name;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}