using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;

namespace Pass.Manager.BL
{
    public class PassCertificateService : BaseService<PassCertificateApple, SearchFilterBase>, IPassCertificateService
    {
        public PassCertificateService(IPassManagerUnitOfWork unitOfWork)
            : base(unitOfWork)
        { 
        }
    }
}
