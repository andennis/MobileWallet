using FluentValidation;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Validators
{
    public class PassProjectFieldViewModelValidator : AbstractValidator<PassProjectFieldViewModel>
    {
        public PassProjectFieldViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}