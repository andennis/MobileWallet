using System.Web.Mvc;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
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

        public ActionResult TabProjects(int id)
        {
            return PartialView("Tabs/_Projects", id);
        }
        public ActionResult TabCertificates(int id)
        {
            return PartialView("Tabs/_Certificates", id);
        }
        public ActionResult TabUsers(int id)
        {
            return PartialView("Tabs/_Users", id);
        }

    }
}