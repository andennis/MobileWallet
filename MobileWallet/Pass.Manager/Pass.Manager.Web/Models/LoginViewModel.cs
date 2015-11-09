using FluentValidation.Attributes;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Validators;

namespace Pass.Manager.Web.Models
{
    [Validator(typeof(LoginViewModelValidator))]
    public class LoginViewModel : BaseLoginViewModel
    {
        public override string DisplayName { get { return "Login"; } }
    }
}
