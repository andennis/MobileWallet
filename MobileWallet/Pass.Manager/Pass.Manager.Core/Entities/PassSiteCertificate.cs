using Common.Repository;

namespace Pass.Manager.Core.Entities
{
    public class PassSiteCertificate : EntityVersionable
    {
        public int PassSiteCertificateId { get; set; }
        public int PassCertificateId { get; set; }
        public int PassSiteId { get; set; }
        public PassCertificate PassCertificate { get; set; }
        public PassSite PassSite { get; set; }
    }
}
