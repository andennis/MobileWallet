using FluentValidation.Attributes;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Validators;

namespace Pass.Manager.Web.Models
{
    [Validator(typeof(UserViewModelValidator))]
    public class UserViewModel : BaseViewModel
    {
        public override string DisplayName { get { return "User"; } }
        public override int EntityId
        {
            get { return UserId; }
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}