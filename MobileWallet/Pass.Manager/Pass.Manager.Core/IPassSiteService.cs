using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;

namespace Pass.Manager.Core
{
    public interface IPassSiteService : IBaseService<PassSite, SearchFilterBase>
    {
        /*
        SearchResult<PassSiteUser> GetUsers(SearchContext searchContext, int passSiteId);
        PassSiteUser GetUser(int passSiteId, int userId);
        void UpdateUser(PassSiteUser siteUser);
        void RemoveUser(int passSiteId, int userId);
        */
    }
}
