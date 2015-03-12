using Common.BL;
using FileStorage.Core;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;

namespace Pass.Manager.BL.Services
{
    public class PassProjectService : BaseService<PassProject, PassProjectFilter, IPassManagerUnitOfWork>, IPassProjectService
    {
        private readonly IFileStorageService _fileStorageService;

        public PassProjectService (IPassManagerUnitOfWork unitOfWork, IFileStorageService fileStorageService)
            : base(unitOfWork)
        {
            _fileStorageService = fileStorageService;
        }

        public override SearchResult<PassProject> Search(SearchContext searchContext, PassProjectFilter searchFilter = null)
        {
            return Search(searchContext, x => x.PassSiteId == searchFilter.PassSiteId);
        }

        public void SavePassContent(int passProjectId)
        {
        }
    }
}
