using System.Collections.Generic;
using Common.BL;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;

namespace Pass.Manager.Core.Services
{
    public interface IPassSiteUserService : IBaseService<PassSiteUser, PassSiteUserFilter>
    {
        IEnumerable<User> GetUnassignedUsers(int passSiteId);
    }
}