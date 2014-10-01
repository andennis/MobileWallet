using System.Collections;
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
    public class PassSiteController : BaseEntityController<PassSiteViewModel, PassSite, IPassSiteService>
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
            return PartialView("Users/_UsersTab", id);
        }

        [AjaxOnly]
        public ActionResult Projects(GridDataRequest request, int passSiteId)
        {
            SearchResult<PassProject> result = _projectService.Search(GridRequestToSearchContext(request), x => x.PassSiteId == passSiteId);
            IEnumerable<PassProjectViewModel> resultView = Mapper.Map<IEnumerable<PassProject>, IEnumerable<PassProjectViewModel>>(result.Data);
            return Json(GridDataResponse.Create(request, resultView, result.TotalCount), JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]
        public ActionResult Users(GridDataRequest request, int passSiteId)
        {
            SearchResult<PassSiteUser> result = _service.GetUsers(GridRequestToSearchContext(request), passSiteId);
            IEnumerable<PassSiteUserViewModel> resultView = Mapper.Map<IEnumerable<PassSiteUser>, IEnumerable<PassSiteUserViewModel>>(result.Data);
            return Json(GridDataResponse.Create(request, resultView, result.TotalCount), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserEdit(int passSiteId, [Bind(Prefix = "id")]int userId)
        {
            PassSiteUser user = _service.GetUser(passSiteId, userId);
            PassSiteUserViewModel userView = Mapper.Map<PassSiteUser, PassSiteUserViewModel>(user);
            return View("Users/UserEdit", userView);
        }

        [HttpPost]
        public ActionResult UserEdit(PassSiteUserViewModel model)
        {
            PassSiteUser siteUser = _service.GetUser(model.PassSiteId, model.UserId);
            siteUser = Mapper.Map<PassSiteUserViewModel, PassSiteUser>(model, siteUser);
            siteUser.User = Mapper.Map<PassSiteUserViewModel, User>(model, siteUser.User);
            _service.UpdateUser(siteUser);
            return RedirectToAction("Edit", new { id = model.PassSiteId });
        }

        [AjaxOnly]
        public ActionResult RemoveUser(int passSiteId, [Bind(Prefix = "id")] int userId)
        {
            _service.RemoveUser(passSiteId, userId);
            return JsonEx();
        }

    }
}