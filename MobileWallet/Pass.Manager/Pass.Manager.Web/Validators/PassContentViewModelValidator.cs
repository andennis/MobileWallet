using FluentValidation;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Validators
{
    public class PassContentViewModelValidator : AbstractValidator<PassContentViewModel>
    {
        public PassContentViewModelValidator()
        {
            RuleFor(x => x.PassContentTemplateId).NotEmpty();
        }
        
    }
}