using FluentValidation;
using Pass.Manager.Web.Areas.Site.Models;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Areas.Site.Validators
{
    public class PassSiteUserViewModelValidator : AbstractValidator<PassSiteUserViewModel>
    {
        public PassSiteUserViewModelValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User must be selected'");
        }
    }
}