using System.Web.Mvc;
using AutoMapper;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Controllers
{
    public class UserController : BaseEntityController<UserViewModel, User, IUserService, SearchFilterBase>
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
            : base(userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult ChangePassword(int id)
        {
            User user = _userService.Get(id);
            UserPasswordViewModel model = Mapper.Map<User, UserPasswordViewModel>(user);
            model.Password = string.Empty;
            model.ConfirmPassword = string.Empty;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(UserPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = _userService.Get(model.UserId);
                user.Password = model.Password;
                _userService.ChangePassword(user);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult EditProfile(string userName)
        {
            User user = _userService.Get(userName);
            UserViewModel model = Mapper.Map<User, UserViewModel>(user);
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = _userService.Get(model.EntityId);
                user = Mapper.Map<UserViewModel, User>(model, user);
                _userService.Update(user);
            }
            return View(model);
        }
        
    }
}