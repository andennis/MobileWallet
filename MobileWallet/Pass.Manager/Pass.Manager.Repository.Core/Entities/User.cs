using Common.Repository;
using System.Collections.Generic;

namespace Pass.Manager.Repository.Core.Entities
{
    public class User : EntityVersionable
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<PassSiteUser> PassSites { get; set; }
    }
}
