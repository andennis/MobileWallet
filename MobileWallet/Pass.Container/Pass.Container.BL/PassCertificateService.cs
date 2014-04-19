using System.IO;
using System.Security.Cryptography.X509Certificates;
using Common.Extensions;
using Pass.CertificateStorage.Core;
using Pass.CertificateStorage.Core.Entities;
using Pass.Container.Core;

namespace Pass.Container.BL
{
    public class PassCertificateService : IPassCertificateService
    {
        private readonly ICertificateStorageService _certificateStorageService;

        public PassCertificateService(ICertificateStorageService certificateStorageService)
        {
            _certificateStorageService = certificateStorageService;
        }

        public X509Certificate2 GetCertificate(int certId)
        {
            using (CertificateInfo certInfo = _certificateStorageService.Read(certId))
            {
                var ms = new MemoryStream();
                certInfo.CertificateFile.CopyTo(ms);
                return new X509Certificate2(ms.ToArray(), certInfo.Password.ConvertToUnsecureString());
            }
        }

        #region
        public void Dispose()
        {
            _certificateStorageService.Dispose();
        }
        #endregion
    }
}
