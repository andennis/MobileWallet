using System.Collections.Generic;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Pass.Manager.Web.Validators;

namespace Pass.Manager.Web.Models
{
     [Validator(typeof(PassSiteCertificateViewModelValidator))]
    public class PassSiteCertificateViewModel : PassCertificateViewModel
    {
        public override int EntityId
        {
            get { return PassSiteCertificateId; }
        }

        public int PassSiteCertificateId { get; set; }
        public int PassSiteId { get; set; }
        public IEnumerable<SelectListItem> Certificates { get; set; }
    }
}