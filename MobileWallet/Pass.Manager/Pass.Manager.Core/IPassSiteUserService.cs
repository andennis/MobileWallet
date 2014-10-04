﻿using System.Collections.Generic;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;

namespace Pass.Manager.Core
{
    public interface IPassSiteUserService : IBaseService<PassSiteUser, PassSiteUserFilter>
    {
        IEnumerable<User> GetUnassignedUsers(int passSiteId);
    }
}