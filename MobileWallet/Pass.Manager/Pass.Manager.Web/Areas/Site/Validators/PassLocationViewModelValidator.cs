using FluentValidation;
using Pass.Manager.Web.Areas.Site.Models;

namespace Pass.Manager.Web.Areas.Site.Validators
{
    public class PassLocationViewModelValidator : AbstractValidator<PassLocationViewModel>
    {
        public PassLocationViewModelValidator()
        {
            RuleFor(x => x.Latitude).NotEmpty();
            RuleFor(x => x.Longitude).NotEmpty();
        }
    }
}