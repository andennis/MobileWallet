using FluentValidation;
using Pass.Manager.Web.Areas.Site.Models;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Areas.Site.Validators
{
    public class PassSiteCertificateViewModelValidator : AbstractValidator<PassSiteCertificateViewModel>
    {
        public PassSiteCertificateViewModelValidator()
        {
            RuleFor(x => x.PassCertificateId).NotEmpty().WithMessage("Certificate must be selected'");
        }
    }
}