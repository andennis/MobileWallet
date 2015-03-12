using System.IO;
using Common.BL;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Core.Services
{
    public interface IPassCertificateService : IBaseService<PassCertificateApple, SearchFilterBase>
    {
        void UploadCertificate(PassCertificateApple passCert, string certPassword, Stream fileStream);
        Stream DownloadCertificate(int certificateStorageId);
    }
}
