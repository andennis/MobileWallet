using Common.Repository;

namespace Pass.Manager.Repository.Core.Entities
{
    public class PassSiteCertificate : EntityVersionable
    {
        public int PassCertificateId { get; set; }
        public int PassSiteId { get; set; }
        public PassCertificate PassCertificate { get; set; }
        public PassSite PassSite { get; set; }
    }
}
