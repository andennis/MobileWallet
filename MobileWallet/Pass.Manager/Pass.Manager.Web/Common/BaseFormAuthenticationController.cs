using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Common
{
    [FormAuthentication]
    public abstract class BaseFormAuthenticationController<TViewModel> : BaseController where TViewModel : BaseLoginViewModel
    {
        protected class UserInfo
        {
            public string UserName { get; set; }
            public int UserId { get; set; }
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [OverrideAuthentication]
        public virtual ActionResult Login(string returnUrl)
        {
            UserLogOff();
            return View(new LoginViewModel() { UserName = string.Empty, RedirectUrl = returnUrl });
        }

        [HttpPost]
        [OverrideAuthentication]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Login(TViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                if (IsAuthenticated(loginViewModel.UserName, loginViewModel.Password))
                {
                    UserInfo user = GetUserInfo(loginViewModel.UserName);
                    this.AuthUserContext.UserId = user.UserId;
                    this.AuthUserContext.UserName = user.UserName;

                    var ticket = new FormsAuthenticationTicket(2, loginViewModel.UserName, DateTime.Now, DateTime.Now.AddMinutes(FormsAuthentication.Timeout.Minutes), false, Session.SessionID);
                    string cryptTicket = FormsAuthentication.Encrypt(ticket);
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, cryptTicket) { Expires = DateTime.Now.AddMinutes(FormsAuthentication.Timeout.Minutes) });

                    return RedirectToLocal(loginViewModel.RedirectUrl);
                }

                ModelState.AddModelError(string.Empty, Resources.Resources.AuthenticationFailed);
            }

            return View(loginViewModel);
        }

        [HttpGet]
        [OverrideAuthentication]
        public virtual ActionResult LogOff()
        {
            UserLogOff();
            return RedirectToAction("Index", "Home");
        }

        protected abstract bool IsAuthenticated(string userName, string password);
        protected abstract UserInfo GetUserInfo(string userName);

        protected virtual void UserLogOff()
        {
            this.AuthUserContext.Clear();
            FormsAuthentication.SignOut();
            HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
        }

        protected virtual ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}