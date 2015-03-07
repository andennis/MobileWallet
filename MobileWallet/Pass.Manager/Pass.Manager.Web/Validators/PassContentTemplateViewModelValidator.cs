using FluentValidation;
using Pass.Manager.Core.Entities;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Validators
{
    public class PassContentTemplateViewModelValidator : AbstractValidator<PassContentTemplateViewModel>
    {
        public PassContentTemplateViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.OrganizationName).NotEmpty();

            RuleFor(x => x.TransitType).NotNull().When(m => m.PassStyle == PassContentStyle.BoardingPass);
            RuleFor(x => x.TransitType).Must(x => !x.HasValue).When(m => m.PassStyle != PassContentStyle.BoardingPass);
        }
    }
}