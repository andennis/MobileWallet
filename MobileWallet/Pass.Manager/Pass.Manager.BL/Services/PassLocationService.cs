using System;
using Common.BL;
using Common.Repository;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;

namespace Pass.Manager.BL.Services
{
    class PassLocationService : BaseService<PassLocation, PassLocationFilter, IPassManagerUnitOfWork>, IPassLocationService
    {
        public PassLocationService(IPassManagerUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override SearchResult<PassLocation> Search(SearchContext searchContext, PassLocationFilter searchFilter = null)
        {
            if (searchFilter == null)
                throw new ArgumentNullException("searchFilter");

            return Search(searchContext, x => x.PassContentTemplateId == searchFilter.PassContentTemplateId);
        }
    }
}