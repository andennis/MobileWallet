using FluentValidation;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Validators
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