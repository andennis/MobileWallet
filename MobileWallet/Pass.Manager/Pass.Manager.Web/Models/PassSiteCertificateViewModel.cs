using System.Collections.Generic;
using System.Web.Mvc;

namespace Pass.Manager.Web.Models
{
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