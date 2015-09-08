using System;


namespace Pass.Manager.Core.Entities
{
    public class PassSiteCertificateView
    {
        public int PassSiteCertificateId { get; set; }
        public int PassCertificateId { get; set; }
        public int PassSiteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PassSiteName { get; set; }
        public string PassSiteDescription { get; set; }
        public DateTime ExpDate { get; set; }
    }
}
