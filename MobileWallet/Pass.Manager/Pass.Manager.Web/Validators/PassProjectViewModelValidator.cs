using FluentValidation;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Validators
{
    public class PassProjectViewModelValidator: AbstractValidator<PassProjectViewModel>
    {
        public PassProjectViewModelValidator ()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}