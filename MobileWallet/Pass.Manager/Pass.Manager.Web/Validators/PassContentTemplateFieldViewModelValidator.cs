using FluentValidation;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Validators
{
    public class PassContentTemplateFieldViewModelValidator : AbstractValidator<PassContentTemplateFieldViewModel>
    {
        public PassContentTemplateFieldViewModelValidator()
        {
            RuleFor(x => x.Label).NotEmpty();
        }
    }
}