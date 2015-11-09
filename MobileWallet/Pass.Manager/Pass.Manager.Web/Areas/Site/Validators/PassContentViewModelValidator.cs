using FluentValidation;
using Pass.Manager.Web.Areas.Site.Models;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Areas.Site.Validators
{
    public class PassContentViewModelValidator : AbstractValidator<PassContentViewModel>
    {
        public PassContentViewModelValidator()
        {
            RuleFor(x => x.PassContentTemplateId).NotEmpty();
        }
        
    }
}