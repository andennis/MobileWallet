using System.Web.Mvc;
using AutoMapper;
using Common.Web;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Areas.Site.Models;
using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Areas.Site.Controllers
{
    public class PassSiteUserController : BaseEntityController<PassSiteUserViewModel, PassSiteUser, IPassSiteUserService, PassSiteUserFilter>
    {
        private readonly IUserService _userService;

        public PassSiteUserController(IPassSiteUserService siteUserService, IUserService userService)
            : base(siteUserService)
        {
            _userService = userService;
        }

        public override ActionResult Create()
        {
            return Create(m =>
            {
                m.PassSiteId = SiteId;
                m.Users = new SelectListTyped<User, int, string>(_service.GetUnassignedUsers(SiteId), x => x.UserId, x => x.UserName);
            });
        }

        public override ActionResult Create(PassSiteUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                PassSiteUser entity = Mapper.Map<PassSiteUserViewModel, PassSiteUser>(model);
                _service.Create(entity);

                User user = _userService.Get(model.UserId);
                user = Mapper.Map<PassSiteUserViewModel, User>(model, user);
                _userService.Update(user);

                return RedirectTo(model);
            }

            PrepareModelToCreateView(model);
            return View(model);
        }

        public override ActionResult Edit(PassSiteUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                PassSiteUser entity = _service.Get(model.EntityId);
                entity = Mapper.Map<PassSiteUserViewModel, PassSiteUser>(model, entity);
                entity.User = Mapper.Map<PassSiteUserViewModel, User>(model, entity.User);
                _service.Update(entity);
                return RedirectTo(model);
            }

            return View(model);
        }

    }
}