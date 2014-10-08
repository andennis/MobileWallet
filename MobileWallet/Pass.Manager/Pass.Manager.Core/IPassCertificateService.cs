using System.IO;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;

namespace Pass.Manager.Core
{
    public interface IPassCertificateService : IBaseService<PassCertificateApple, SearchFilterBase>
    {
        void UploadCertificate(PassCertificateApple passCert, string certPassword, Stream fileStream);
    }
}
