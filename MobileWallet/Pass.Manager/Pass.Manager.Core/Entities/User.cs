﻿using System.Collections.Generic;
using Common.Repository;

namespace Pass.Manager.Core.Entities
{
    public class User : EntityVersionable
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public ICollection<PassSiteUser> PassSites { get; set; }
    }
}
