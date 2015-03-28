using Common.BL;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Core.Services
{
    public interface IPassSiteService : IPassManagerServiceBase<PassSite, SearchFilterBase>
    {
        /*
        SearchResult<PassSiteUser> GetUsers(SearchContext searchContext, int passSiteId);
        PassSiteUser GetUser(int passSiteId, int userId);
        void UpdateUser(PassSiteUser siteUser);
        void RemoveUser(int passSiteId, int userId);
        */
    }
}
