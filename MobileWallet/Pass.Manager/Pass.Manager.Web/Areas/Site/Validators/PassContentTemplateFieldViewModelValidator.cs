using FluentValidation;
using Pass.Manager.Web.Areas.Site.Models;

namespace Pass.Manager.Web.Areas.Site.Validators
{
    public class PassContentTemplateFieldViewModelValidator : AbstractValidator<PassContentTemplateFieldViewModel>
    {
        public PassContentTemplateFieldViewModelValidator()
        {
            RuleFor(x => x.FieldKind).NotEmpty().WithLocalizedName(() => Resources.Resources.FieldLocation);
            When(x => !x.PassProjectFieldId.HasValue, () => 
                RuleFor(x => x.Value).NotEmpty().WithLocalizedName(() => Resources.Resources.Value));
        }
    }
}