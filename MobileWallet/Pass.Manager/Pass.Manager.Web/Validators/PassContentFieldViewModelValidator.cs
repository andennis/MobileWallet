using FluentValidation;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Validators
{
    public class PassContentFieldViewModelValidator : AbstractValidator<PassContentFieldViewModel>
    {
        public PassContentFieldViewModelValidator()
        {
            RuleFor(x => x.FieldValue).NotEmpty().WithLocalizedName(() => Resources.Resources.Value);
        }
        
    }
}