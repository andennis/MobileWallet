using System;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;

namespace Pass.Manager.BL.Services
{
    public class PassProjectFieldService : BaseService<PassProjectField, PassProjectFieldFilter, IPassManagerUnitOfWork>, IPassProjectFieldService
    {
        public PassProjectFieldService(IPassManagerUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            
        }

        public override SearchResult<PassProjectField> Search(SearchContext searchContext, PassProjectFieldFilter searchFilter = null)
        {
            if (searchFilter == null)
                throw new ArgumentNullException("searchFilter");

            return Search(searchContext, x => x.PassProjectId == searchFilter.PassProjectId);
        }

    }
}
