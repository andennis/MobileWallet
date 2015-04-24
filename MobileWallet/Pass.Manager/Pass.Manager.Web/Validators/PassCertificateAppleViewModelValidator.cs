using FluentValidation;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Validators
{
    public class PassCertificateAppleViewModelValidator : AbstractValidator<PassCertificateAppleViewModel>
    {
        public PassCertificateAppleViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.PassTypeId).NotEmpty();
            RuleFor(x => x.TeamId).NotEmpty();
            RuleFor(x => x.Password).NotEmpty().When(x => x.CertificateFile != null);
        }
    }
}