using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;
using Common.Extensions;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult CreateUser(string returnUrl)
        {
            return View(new CreateUserViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(CreateUserViewModel createUserViewModel, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (createUserViewModel.Password == createUserViewModel.ConfirmPassword)
                    {
                        var user = new UserInfo
                                        {
                                            UserName = createUserViewModel.UserName,
                                            FirstName = createUserViewModel.FirstName,
                                            LastName = createUserViewModel.LastName,
                                            Password = createUserViewModel.Password.ConvertToSecureString()
                                        };
                        _userService.Create(user);
                        ModelState.Add("User was created.", new ModelState());
                    }
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Create user has failed.");
            }
            return View(createUserViewModel);
        }
    }
}