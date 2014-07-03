using Common.Repository;
using System.Collections.Generic;

namespace Pass.Manager.Repository.Core.Entities
{
    public class PassSite : EntityVersionable
    {
        public string Name { get; set; }
        public ICollection<PassSiteUser> Users { get; set; }
        public ICollection<PassCertificate> Certificates { get; set; }
    }
}
