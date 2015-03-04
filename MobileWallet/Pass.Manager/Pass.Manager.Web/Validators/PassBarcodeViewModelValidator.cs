using FluentValidation;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Validators
{
    public class PassBarcodeViewModelValidator : AbstractValidator<PassBarcodeViewModel>
    {
        public PassBarcodeViewModelValidator()
        {
            RuleFor(x => x.Format).NotEmpty();
            RuleFor(x => x.Message).NotEmpty();
            RuleFor(x => x.MessageEncoding).NotEmpty();
        }
    }
}