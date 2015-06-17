using Common.BL;
using Common.Repository;
using Common.Utils;
using Pass.Container.Core.Entities;
using Pass.Container.Core.SearchFilters;

namespace Pass.Manager.Core.Services
{
    public interface IPassOnlineService
    {
        int Register(int passContentId);
        void UpdateOnline(int passContentId);
        FileContentInfo GetPassPackage(int passContentId);
        SearchResult<RegistrationInfo> GetPassRegistrations(SearchContext searchContext, PassRegistrationFilter filter);
    }
}