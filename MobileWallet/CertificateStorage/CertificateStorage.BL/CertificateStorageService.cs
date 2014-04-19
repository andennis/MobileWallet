using System;
using System.IO;
using CertificateStorage.Core;
using CertificateStorage.Core.Entities;
using CertificateStorage.Repository.Core;
using CertificateStorage.Repository.Core.Entities;
using Common.Extensions;
using Common.Repository;
using Common.Utils;
using FileStorage.Core;

namespace CertificateStorage.BL
{
    public class CertificateStorageService : ICertificateStorageService
    {
        private const string SecurityVector = "142b1a)v(b#Oc&Mq";

        private readonly ICertificateStorageConfig _config;
        private readonly IFileStorageService _fileStorageService;
        private readonly ICertificateStorageUnitOfWork _unitOfWork;
        private readonly IRepository<Certificate> _certRepository;

        public CertificateStorageService(ICertificateStorageConfig config, 
            ICertificateStorageUnitOfWork unitOfWork,
            IFileStorageService fileStorageService)
        {
            _config = config;
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;

            _certRepository = _unitOfWork.GetRepository<Certificate>();
        }

        public int Put(CertificateInfo certInfo)
        {
            if (certInfo == null)
                throw new ArgumentNullException("certInfo");
            if (certInfo.CertificateFile == null)
                throw new CertificateStorageException("Certificate file is not specified");

            var cert = new Certificate() { Name = certInfo.Name };
            string psw = certInfo.Password.ConvertToUnsecureString();
            cert.Password = Crypto.EncryptString(psw, _config.SecurityKey, SecurityVector);
            cert.Status = EntityStatus.Active;
            cert.FileId = _fileStorageService.PutFile(certInfo.CertificateFile);
            _certRepository.Insert(cert);
            _unitOfWork.Save();

            return cert.CertificateId;
        }

        public CertificateInfo Read(int certId)
        {
            Certificate cert = _certRepository.Find(certId);
            if (cert == null)
                throw new CertificateStorageException(string.Format("Certificate ID:{0} not found", certId));
            if (cert.Status == EntityStatus.Deleted)
                throw new CertificateStorageException(string.Format("Certificate ID:{0} was deleted", certId));

            var certInfo = new CertificateInfo() {Name = cert.Name};
            string psw = Crypto.DecryptString(cert.Password, _config.SecurityKey, SecurityVector);
            certInfo.Password = psw.ConvertToSecureString();
            string certFilePath = _fileStorageService.GetStorageItemPath(cert.FileId);
            certInfo.CertificateFile = new FileStream(certFilePath, FileMode.Open, FileAccess.Read);

            return certInfo;
        }

        public void Update(int certId, CertificateInfo certInfo)
        {
            if (certInfo == null)
                throw new ArgumentNullException("certInfo");

            Certificate cert = _certRepository.Find(certId);
            if (cert == null)
                throw new CertificateStorageException(string.Format("Certificate ID:{0} not found", certId));
            if (cert.Status == EntityStatus.Deleted)
                throw new CertificateStorageException(string.Format("Certificate ID:{0} was deleted", certId));

            cert.Name = certInfo.Name;
            string psw = certInfo.Password.ConvertToUnsecureString();
            cert.Password = Crypto.EncryptString(psw, _config.SecurityKey, SecurityVector);

            int oldFileId = cert.FileId;
            if (certInfo.CertificateFile != null)
                cert.FileId = _fileStorageService.PutFile(certInfo.CertificateFile);

            _certRepository.Update(cert);
            _unitOfWork.Save();

            if (certInfo.CertificateFile != null)
                _fileStorageService.DeleteStorageItem(oldFileId);
        }

        public void Delete(int certId)
        {
            Certificate cert = _certRepository.Find(certId);
            if (cert == null)
                return;

            _certRepository.Delete(cert);
            _unitOfWork.Save();

            _fileStorageService.DeleteStorageItem(cert.FileId);
        }

        #region IDisposable
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
        #endregion
    }
}
