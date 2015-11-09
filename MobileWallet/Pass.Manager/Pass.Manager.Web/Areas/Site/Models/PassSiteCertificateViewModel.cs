using System.Collections.Generic;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Pass.Manager.Web.Areas.Site.Validators;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Areas.Site.Models
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
        public string PassSiteName { get; set; }
        public string PassSiteDescription { get; set; }
        public IEnumerable<SelectListItem> Certificates { get; set; }
    }
}