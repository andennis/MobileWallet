using FluentValidation.Attributes;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Validators;

namespace Pass.Manager.Web.Models
{
    [Validator(typeof(UserPasswordValidator))]
    public class UserPasswordViewModel : UserViewModel
    {
        public override string DisplayName { get { return "User password"; } }
        
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

}