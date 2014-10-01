using Pass.Manager.Core.Entities;

namespace Pass.Manager.Core
{
    public interface IPassSiteService : IBaseService<PassSite>
    {
        SearchResult<PassSiteUser> GetUsers(SearchContext searchContext, int passSiteId);
        PassSiteUser GetUser(int passSiteId, int userId);
        void UpdateUser(PassSiteUser siteUser);
        void RemoveUser(int passSiteId, int userId);
    }
}
