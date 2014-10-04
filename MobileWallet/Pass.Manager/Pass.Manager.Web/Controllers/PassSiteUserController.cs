using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Common.Web;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Controllers
{
    public class PassSiteUserController : BaseEntityController<PassSiteUserViewModel, PassSiteUser, IPassSiteUserService, PassSiteUserFilter>
    {
        public PassSiteUserController(IPassSiteUserService siteUserService)
            : base(siteUserService)
        {
        }

        public ActionResult AddUser(int passSiteId)
        {
            var model = new PassSiteUserViewModel() { PassSiteId = passSiteId };
            SetDefaultReturnUrl(model);
            model.Users = new SelectTListTyped<User, int, string>(_service.GetUnassignedUsers(passSiteId), x => x.UserId, x => x.UserName);
            return View("Create", model);
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