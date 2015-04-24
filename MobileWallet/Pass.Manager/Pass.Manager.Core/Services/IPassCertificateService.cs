using System.IO;
using Common.BL;
using Common.Utils;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Core.Services
{
    public interface IPassCertificateService : IPassManagerServiceBase<PassCertificateApple, SearchFilterBase>
    {
        void UploadCertificate(PassCertificateApple passCert, string certPassword, FileContentInfo fileContent);
        Stream DownloadCertificate(int certificateStorageId);
    }
}
