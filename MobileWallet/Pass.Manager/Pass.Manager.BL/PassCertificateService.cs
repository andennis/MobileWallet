using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Repository.Core;

namespace Pass.Manager.BL
{
    public class PassCertificateService: BaseService<PassCertificate>, IPassCertificateService
    {
        public PassCertificateService(IPassManagerUnitOfWork unitOfWork)
            : base(unitOfWork)
        { 
        }
    }
}
