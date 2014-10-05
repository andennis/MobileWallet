using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;

namespace Pass.Manager.BL
{
    class PassProjectService : BaseService<PassProject, PassProjectFilter, IPassManagerUnitOfWork>, IPassProjectService
    {
        public PassProjectService (IPassManagerUnitOfWork unitOfWork)
            : base(unitOfWork)
        { 
        }

        public override SearchResult<PassProject> Search(SearchContext searchContext, PassProjectFilter searchFilter = null)
        {
            return Search(searchContext, x => x.PassSiteId == searchFilter.PassSiteId);
        }
    }
}
