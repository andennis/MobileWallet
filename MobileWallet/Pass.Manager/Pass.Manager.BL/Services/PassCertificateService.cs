using System.IO;
using CertificateStorage.Core;
using CertificateStorage.Core.Entities;
using Common.BL;
using Common.Extensions;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.Services;

namespace Pass.Manager.BL.Services
{
    public class PassCertificateService : PassManagerServiceBase<PassCertificateApple, SearchFilterBase>, IPassCertificateService
    {
        private readonly ICertificateStorageService _certificateStorageService;

        public PassCertificateService(IPassManagerUnitOfWork unitOfWork, ICertificateStorageService certificateStorageService)
            : base(unitOfWork)
        {
            _certificateStorageService = certificateStorageService;
        }

        public void UploadCertificate(PassCertificateApple passCert, string certPassword, Stream fileStream)
        {
            var certInfo = new CertificateInfo
                           {
                               CertificateId = passCert.CertificateStorageId,
                               Name = string.Format("TID#{0}/PTID#{1}", passCert.TeamId, passCert.PassTypeId),
                               Password = certPassword.ConvertToSecureString(),
                               CertificateFile = fileStream
                           };

            if (passCert.CertificateStorageId == 0)
                passCert.CertificateStorageId = _certificateStorageService.Put(certInfo);
            else
                _certificateStorageService.Update(certInfo);
        }

        public Stream DownloadCertificate(int certificateStorageId)
        {
            CertificateInfo certInfo = _certificateStorageService.Read(certificateStorageId);
            return certInfo.CertificateFile;
        }
    }
}
