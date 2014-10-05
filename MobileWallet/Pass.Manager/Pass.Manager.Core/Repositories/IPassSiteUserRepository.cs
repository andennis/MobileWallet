using System.Collections.Generic;
using Common.Repository;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Core.Repositories
{
    public interface IPassSiteUserRepository : IRepository<PassSiteUser>
    {
        IEnumerable<User> GetUnassignedUsers(int passSiteId);
    }
}