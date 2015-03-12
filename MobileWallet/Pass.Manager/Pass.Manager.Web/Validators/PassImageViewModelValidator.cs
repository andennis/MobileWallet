using FluentValidation;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Validators
{
    public class PassImageViewModelValidator : AbstractValidator<PassImageViewModel>
    {
        public PassImageViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}