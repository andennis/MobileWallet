using Common.Repository;
using System.Collections.Generic;

namespace Pass.Manager.Repository.Core.Entities
{
    public class PassSite : EntityVersionable
    {
        public int PassSiteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<PassSiteUser> Users { get; set; }
        public ICollection<PassSiteCertificate> Certificates { get; set; }
        public ICollection<PassProject> Projects { get; set; }
    }
}
