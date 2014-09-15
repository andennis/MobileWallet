using FluentValidation.Attributes;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Validators;

namespace Pass.Manager.Web.Models
{
    [Validator(typeof(UserViewModelValidator))]
    public class UserViewModel : BaseViewModel
    {
        public override string DisplayName { get { return "Create user"; } }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public override int EntityId
        {
            get { return UserId; }
        }
    }
}