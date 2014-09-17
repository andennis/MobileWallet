using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Common.Extensions;
using Common.Utils;
using FluentValidation.Results;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Models;
using Pass.Manager.Web.Validators;

namespace Pass.Manager.Web.Controllers
{
    public class UserController : BaseEntityController<UserViewModel, User>
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