using System;
using System.Linq;
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

        public override SearchResult<PassContentTemplate> Search(SearchContext searchContext, PassContentTemplateFilter searchFilter)
        {
            if (searchFilter == null)
                throw new ArgumentNullException("searchFilter");

            return Search(searchContext, x => x.PassProjectId == searchFilter.PassProjectId);
        }

        public PassContentTemplate GetDetails(int entityId)
        {
            return _repository.Query()
                .Filter(x => x.PassContentTemplateId == entityId)
                .Include(x => x.Barcode)
                .Include(x => x.Beacons)
                .Include(x => x.Locations)
                .Include(x => x.PassProject)
                .Include(x => x.PassContentTemplateFields.Select(x2 => x2.PassProjectField))
                .Include(x => x.PassImages)
                .Get().FirstOrDefault();
        }
    }
}
