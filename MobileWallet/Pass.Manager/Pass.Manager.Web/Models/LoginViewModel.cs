using FluentValidation.Attributes;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Validators;

namespace Pass.Manager.Web.Models
{
    [Validator(typeof(LoginViewModelValidator))]
    public class LoginViewModel : BaseViewModel
    {
        public override string DisplayName { get { return "Login"; } }
        public string UserName { get; set; }
        public string Password { get; set; }
        //public bool RememberMe { get; set; }
    }
}
