using System.Collections.Generic;
using Common.Repository;

namespace Pass.Manager.Core.Entities
{
    public class PassSite : EntityVersionable, IEntityWithId
    {
        public int EntityId { get { return PassSiteId; } }

        public int PassSiteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<PassSiteUser> Users { get; set; }
        public ICollection<PassSiteCertificate> Certificates { get; set; }
        public ICollection<PassProject> Projects { get; set; }
    }
}
