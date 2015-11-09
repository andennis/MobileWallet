using FluentValidation;
using Pass.Manager.Web.Areas.Site.Models;

namespace Pass.Manager.Web.Areas.Site.Validators
{
    public class PassProjectViewModelValidator: AbstractValidator<PassProjectViewModel>
    {
        public PassProjectViewModelValidator ()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}