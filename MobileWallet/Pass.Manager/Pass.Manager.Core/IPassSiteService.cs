using Pass.Manager.Core.Entities;

namespace Pass.Manager.Core
{
    public interface IPassSiteService : IBaseService<PassSite>
    {
        SearchResult<PassSiteUser> GetUsers(SearchContext searchContext, int passSiteId);
    }
}
