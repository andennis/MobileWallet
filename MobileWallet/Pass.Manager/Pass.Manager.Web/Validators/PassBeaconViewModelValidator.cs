using FluentValidation;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Validators
{
    public class PassBeaconViewModelValidator : AbstractValidator<PassBeaconViewModel>
    {
        public PassBeaconViewModelValidator()
        {
            RuleFor(x => x.ProximityUuid).NotEmpty();
        }
    }
}