using FluentValidation.Attributes;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Validators;

namespace Pass.Manager.Web.Models
{
    [Validator(typeof(UserPasswordValidator))]
    public class UserPasswordViewModel : BaseViewModel
    {
        public override string DisplayName { get { return "Change Password"; } }
        public int UserId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

}