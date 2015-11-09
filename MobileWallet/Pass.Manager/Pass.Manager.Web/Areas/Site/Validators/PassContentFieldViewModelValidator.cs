using FluentValidation;
using Pass.Manager.Web.Areas.Site.Models;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Areas.Site.Validators
{
    public class PassContentFieldViewModelValidator : AbstractValidator<PassContentFieldViewModel>
    {
        public PassContentFieldViewModelValidator()
        {
            //RuleFor(x => x.FieldValue).NotEmpty().WithLocalizedName(() => Resources.Resources.Value);
        }
        
    }
}