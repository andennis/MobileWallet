using System.Collections.Generic;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;

namespace Pass.Manager.Core.Services
{
    public interface IPassSiteUserService : IPassManagerServiceBase<PassSiteUser, PassSiteUserFilter>
    {
        IEnumerable<User> GetUnassignedUsers(int passSiteId);
    }
}