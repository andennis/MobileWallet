using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Common.Web.Grid;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Controllers
{
    public class PassSiteController : BaseEntityController<PassSiteViewModel, PassSite>
    {
        private readonly IPassProjectService _projectService;

        public PassSiteController(IPassSiteService siteService, IPassProjectService projectService)
            : base(siteService)
        {
            _projectService = projectService;
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

        [AjaxOnly]
        public ActionResult Projects(GridDataRequest request, int? passSiteId)
        {
            SearchResult<PassProject> result = _projectService.Search(GridRequestToSearchContext(request), x => x.PassSiteId == passSiteId);
            IEnumerable<PassProjectViewModel> resultView = Mapper.Map<IEnumerable<PassProject>, IEnumerable<PassProjectViewModel>>(result.Data);
            return Json(GridDataResponse.Create(request, resultView, result.TotalCount), JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]
        public ActionResult Users(GridDataRequest request, int? passSiteId)
        {
             
            SearchResult<PassProject> result = _projectService.Search(GridRequestToSearchContext(request), x => x.PassSiteId == passSiteId);
            IEnumerable<PassProjectViewModel> resultView = Mapper.Map<IEnumerable<PassProject>, IEnumerable<PassProjectViewModel>>(result.Data);
            return Json(GridDataResponse.Create(request, resultView, result.TotalCount), JsonRequestBehavior.AllowGet);
        }

    }
}