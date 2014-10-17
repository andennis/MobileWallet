using System;
using System.Collections.Generic;
using Common.Repository;

namespace Pass.Manager.Core.Entities
{
    public class PassCertificate : EntityVersionable, IEntityWithId
    {
        public int EntityId { get { return PassCertificateId; } }

        public int PassCertificateId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ExpDate { get; set; }
        public string CertificateFileName { get; set; }
        public int CertificateStorageId { get; set; }
        public ICollection<PassSiteCertificate> PassSites { get; set; }
    }
}
