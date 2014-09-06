using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;
using Common.Extensions;
using Common.Utils;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Models;

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
        [AllowAnonymous]
        public ActionResult Manage(string returnUrl)
        {
            return View(new UserViewModel());
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Create(string returnUrl)
        {
            return View(new UserViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel userViewModel, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (userViewModel.Password == userViewModel.ConfirmPassword)
                    {
                        var user = new User
                                        {
                                            UserName = userViewModel.UserName,
                                            FirstName = userViewModel.FirstName,
                                            LastName = userViewModel.LastName,
                                            Password = Crypto.CalculateHash(userViewModel.UserName, userViewModel.Password)
                                        };
                        _userService.Create(user);
                        ViewBag.CreateUserResult = "User was created.";
                    }
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Create user has failed.");
            }
            return View(userViewModel);
        }
    }
}