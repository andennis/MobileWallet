using System.Web.Mvc;
using Common.BL;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Controllers
{
    public class PassSiteController : BaseEntityController<PassSiteViewModel, PassSite, IPassSiteService, SearchFilterBase>
    {
        public PassSiteController(IPassSiteService siteService)
            : base(siteService)
        {
        }

        public override ActionResult Index()
        {
            AuthUserContext.UserData.SiteId = null;
            return base.Index();
        }

        public ActionResult Enter(int id)
        {
            AuthUserContext.UserData.SiteId = id;
            return RedirectToAction("Index", "PassProject", new { area = "Site", PassSiteId = id });
        }

    }
}