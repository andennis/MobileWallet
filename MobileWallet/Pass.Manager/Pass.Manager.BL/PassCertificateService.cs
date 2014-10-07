using CertificateStorage.Core;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;

namespace Pass.Manager.BL
{
    public class PassCertificateService : BaseService<PassCertificateApple, SearchFilterBase, IPassManagerUnitOfWork>, IPassCertificateService
    {
        private readonly ICertificateStorageService _certificateStorageService;

        public PassCertificateService(IPassManagerUnitOfWork unitOfWork, ICertificateStorageService certificateStorageService)
            : base(unitOfWork)
        {
            _certificateStorageService = certificateStorageService;
        }
    }
}
