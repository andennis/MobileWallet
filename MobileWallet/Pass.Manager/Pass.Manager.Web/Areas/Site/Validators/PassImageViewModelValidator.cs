using FluentValidation;
using Pass.Manager.Web.Areas.Site.Models;

namespace Pass.Manager.Web.Areas.Site.Validators
{
    public class PassImageViewModelValidator : AbstractValidator<PassImageViewModel>
    {
        public PassImageViewModelValidator()
        {
            //RuleFor(x => x.FileStorageId).NotEmpty();
            //When(m => m.IsNew, );

        }
    }
}