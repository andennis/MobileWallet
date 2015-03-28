using System;
using Common.BL;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;

namespace Pass.Manager.BL.Services
{
    public class PassContentTemplateService : PassManagerServiceBase<PassContentTemplate, PassContentTemplateFilter>, IPassContentTemplateService
    {
        public PassContentTemplateService(IPassManagerUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override SearchResult<PassContentTemplate> Search(SearchContext searchContext, PassContentTemplateFilter searchFilter = null)
        {
            if (searchFilter == null)
                throw new ArgumentNullException("searchFilter");

            return Search(searchContext, x => x.PassProjectId == searchFilter.PassProjectId);
        }

    }
}
