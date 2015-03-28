using Common.BL;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.Services;

namespace Pass.Manager.BL.Services
{
    public class PassSiteService : PassManagerServiceBase<PassSite, SearchFilterBase>, IPassSiteService
    {
        public PassSiteService(IPassManagerUnitOfWork unitOfWork)
            : base(unitOfWork)
        { 
        }

    }
}
