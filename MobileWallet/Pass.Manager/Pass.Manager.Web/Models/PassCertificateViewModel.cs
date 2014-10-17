using System;
using System.Web;
using FluentValidation.Attributes;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Validators;

namespace Pass.Manager.Web.Models
{
    [Validator(typeof(PassCertificateViewModelValidator))]
    public abstract class PassCertificateViewModel : BaseViewModel
    {
        public override string DisplayName { get { return "Pass Certificate"; } }
        public override int EntityId { get { return PassCertificateId; } }

        public int PassCertificateId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ExpDate { get; set; }
        public string CertificateFileName { get; set; }
        public string Password { get; set; }
        public int CertificateStorageId { get; set; }
        public HttpPostedFileBase CertificateFile { get; set; }
    }

}