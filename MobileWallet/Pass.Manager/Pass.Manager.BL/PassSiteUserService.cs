using Pass.Manager.Core;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.BL
{
    public class PassSiteUserService : BaseService<PassSiteUser>, IPassSiteUserService
    {
        public PassSiteUserService(IPassManagerUnitOfWork unitOfWork)
            : base(unitOfWork)
        { 
        }
    }
}
