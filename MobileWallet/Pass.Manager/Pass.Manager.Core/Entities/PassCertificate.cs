using Common.Repository;
using System;
using System.Collections.Generic;

namespace Pass.Manager.Repository.Core.Entities
{
    public class PassCertificate : EntityVersionable
    {
        public int PassCertificateId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ExpDate { get; set; }
        public int CertificateStorageId { get; set; }
        public ICollection<PassSiteCertificate> PassSites { get; set; }
    }
}
