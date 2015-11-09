using Pass.Manager.Core.Entities;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Controllers
{
    public class AccountController : BaseFormAuthenticationController<LoginViewModel>
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        protected override bool IsAuthenticated(string userName, string password)
        {
            return _userService.IsAuthenticated(userName, password);
        }

        protected override UserInfo GetUserInfo(string userName)
        {
            User user = _userService.Get(userName);
            return new UserInfo() {UserId = user.UserId, UserName = user.UserName};
        }
    }
}