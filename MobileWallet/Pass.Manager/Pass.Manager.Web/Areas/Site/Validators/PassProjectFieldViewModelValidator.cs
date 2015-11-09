using FluentValidation;
using Pass.Manager.Web.Areas.Site.Models;

namespace Pass.Manager.Web.Areas.Site.Validators
{
    public class PassProjectFieldViewModelValidator : AbstractValidator<PassProjectFieldViewModel>
    {
        public PassProjectFieldViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}