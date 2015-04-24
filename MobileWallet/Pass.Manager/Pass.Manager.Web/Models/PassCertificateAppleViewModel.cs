using FluentValidation.Attributes;
using Pass.Manager.Web.Validators;

namespace Pass.Manager.Web.Models
{
    [Validator(typeof(PassCertificateAppleViewModelValidator))]
    public class PassCertificateAppleViewModel : PassCertificateViewModel
    {
        public string TeamId { get; set; }
        public string PassTypeId { get; set; }
    }
}