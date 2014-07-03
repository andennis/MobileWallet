using Common.Repository;
using System;

namespace Pass.Manager.Repository.Core.Entities
{
    public class PassCertificate : EntityVersionable
    {
        public int PassCertificateId { get; set; }
        public int ClientSiteId { get; set; }
        public string Name { get; set; }
        public DateTime ExpDate { get; set; }
        public PassSite ClientSite { get; set; }

    }
}
