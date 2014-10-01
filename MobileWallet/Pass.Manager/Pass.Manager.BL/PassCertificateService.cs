using Pass.Manager.Core;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.BL
{
    public class PassCertificateService: BaseService<PassCertificateApple>, IPassCertificateService
    {
        public PassCertificateService(IPassManagerUnitOfWork unitOfWork)
            : base(unitOfWork)
        { 
        }
    }
}
