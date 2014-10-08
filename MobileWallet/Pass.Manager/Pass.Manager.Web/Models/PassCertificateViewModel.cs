using System;
using System.Web;
using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Models
{
    public abstract class PassCertificateViewModel : BaseViewModel
    {
        public override string DisplayName { get { return "Pass Certificate"; } }
        public override int EntityId { get { return PassCertificateId; } }

        public int PassCertificateId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ExpDate { get; set; }
        public string Password { get; set; }
        public int CertificateStorageId { get; set; }
        public HttpPostedFileBase CertificateFile { get; set; }
    }

}