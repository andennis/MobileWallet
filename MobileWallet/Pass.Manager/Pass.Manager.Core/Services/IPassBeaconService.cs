using Common.BL;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;

namespace Pass.Manager.Core.Services
{
    public interface IPassBeaconService : IPassManagerServiceBase<PassBeacon, PassBeaconFilter>
    {
        SearchResult<PassBeacon> Search(SearchContext searchContext, PassBeaconFilter searchFilter = null);
    }
}
