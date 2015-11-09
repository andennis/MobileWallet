using System;
using Common.BL;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;

namespace Pass.Manager.BL.Services
{
    public class PassBeaconService : BaseService<PassBeacon, PassBeaconFilter, IPassManagerUnitOfWork>, IPassBeaconService
    {
        public PassBeaconService(IPassManagerUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override SearchResult<PassBeacon> Search(SearchContext searchContext, PassBeaconFilter searchFilter)
        {
            if (searchFilter == null)
                throw new ArgumentNullException("searchFilter");

            return Search(searchContext, x => x.PassContentTemplateId == searchFilter.PassContentTemplateId);
        }
    }
}
