using System;
using System.Web.Mvc;
using System.Web.Security;
using Common.Extensions;
using Common.Utils;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel() { UserName = "" });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginViewModel, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //User user = _userService.Get(loginViewModel.UserName);
                    //if (user != null && loginViewModel.Password == Crypto.CalculateHash(user.UserName.ToLower(), user.Password))
                    {
                        FormsAuthentication.SetAuthCookie(loginViewModel.UserName, false);
                        return RedirectToLocal(returnUrl);
                    }
                }
            }
            catch
            {
                ModelState.AddModelError("", "Invalid username or password.");
            }
            
            return View(loginViewModel);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

    }
}