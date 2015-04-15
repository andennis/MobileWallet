using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;

namespace Pass.Manager.BL.Services
{
    public class PassContentService : PassManagerServiceBase<PassContent, PassContentFilter>, IPassContentService
    {
        public PassContentService(IPassManagerUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

    }
}
