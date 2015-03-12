using System.Collections.Generic;
using System.Linq;
using Common.BL;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;

namespace Pass.Manager.BL.Services
{
    public class PassSiteCertificateService : BaseService<PassSiteCertificate, PassSiteCertificateFilter, IPassManagerUnitOfWork>, IPassSiteCertificateService
    {
        public PassSiteCertificateService(IPassManagerUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override PassSiteCertificate Get(int entityId)
        {
            return _repository.Query().Filter(x => x.PassSiteCertificateId == entityId).Include(x => x.PassCertificate).Get().First();
        }

        public IEnumerable<PassCertificate> GetUnassignedCertificates(int passSiteId)
        {
            return _unitOfWork.PassSiteCertificateRepository.GetUnassignedCertificates(passSiteId);
        }

        public override SearchResult<PassSiteCertificate> Search(SearchContext searchContext, PassSiteCertificateFilter searchFilter = null)
        {
            IEnumerable<PassSiteCertificate> data = _repository.Query()
                .Filter(x => x.PassSiteId == searchFilter.PassSiteId)
                .Include(x => x.PassCertificate)
                .Get();

            return new SearchResult<PassSiteCertificate>()
                   {
                       Data = data,
                       TotalCount = data.Count()
                   };
        }
    }
}
