using System.Web.Mvc;
using System.Web.Security;
using Pass.Manager.Core.Services;
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
            return View(new LoginViewModel() { UserName = "", RedirectUrl = returnUrl});
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                if (_userService.IsAuthenticated(loginViewModel.UserName, loginViewModel.Password))
                {
                    FormsAuthentication.SetAuthCookie(loginViewModel.UserName, false);
                    return RedirectToLocal(loginViewModel.RedirectUrl);
                }

                ModelState.AddModelError("", Resources.Resources.AuthenticationFailed);
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

            return RedirectToAction("Index", "Home");
        }

    }
}