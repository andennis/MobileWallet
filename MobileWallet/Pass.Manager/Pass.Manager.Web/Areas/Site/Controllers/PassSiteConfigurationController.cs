using Common.BL;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Areas.Site.Models;
using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Areas.Site.Controllers
{
    public class PassSiteConfigurationController : BaseEntityController<PassSiteConfigurationViewModel, PassSite, IPassSiteService, SearchFilterBase>
    {
        public PassSiteConfigurationController(IPassSiteService siteService)
            : base(siteService)
        {
        }

    }
}