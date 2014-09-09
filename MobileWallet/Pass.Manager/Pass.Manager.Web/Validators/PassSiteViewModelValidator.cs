using FluentValidation;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Validators
{
    public class PassSiteViewModelValidator : AbstractValidator<PassSiteViewModel>
    {
        public PassSiteViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}