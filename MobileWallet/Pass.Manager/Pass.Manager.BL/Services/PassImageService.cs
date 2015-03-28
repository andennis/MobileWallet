using System;
using Common.BL;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;

namespace Pass.Manager.BL.Services
{
    public class PassImageService : PassManagerServiceBase<PassImage, PassImageFilter>, IPassImageService
    {
        public PassImageService(IPassManagerUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override SearchResult<PassImage> Search(SearchContext searchContext, PassImageFilter searchFilter = null)
        {
            if (searchFilter == null)
                throw new ArgumentNullException("searchFilter");

            return Search(searchContext, x => x.PassContentTemplateId == searchFilter.PassContentTemplateId);
        }

    }
}
