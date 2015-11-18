using FluentValidation;
using Pass.Manager.Web.Areas.Site.Models;

namespace Pass.Manager.Web.Areas.Site.Validators
{
    public class PassSiteConfigurationViewModelValidator : AbstractValidator<PassSiteConfigurationViewModel>
    {
        public PassSiteConfigurationViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }

    }
}