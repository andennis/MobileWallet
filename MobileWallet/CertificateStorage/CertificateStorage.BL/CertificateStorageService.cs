using System;
using System.IO;
using System.Linq;
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
            cert.FileId = _fileStorageService.Put(certInfo.CertificateFile);
            _certRepository.Insert(cert);
            _unitOfWork.Save();

            certInfo.CertificateId = cert.CertificateId;
            return cert.CertificateId;
        }

        public CertificateInfo Read(int certId)
        {
            Certificate cert = _certRepository.Find(certId);
            if (cert == null)
                throw new CertificateStorageException(string.Format("Certificate ID:{0} not found", certId));
            if (cert.Status == EntityStatus.Deleted)
                throw new CertificateStorageException(string.Format("Certificate ID:{0} was deleted", certId));

            return ConvertTo(cert);
        }

        public CertificateInfo Read(string certName)
        {
            if (certName == null)
                throw new ArgumentNullException("certName");

            Certificate cert = _certRepository.Query().Filter(x => x.Name == certName).Get().FirstOrDefault();
            if (cert == null)
                throw new CertificateStorageException(string.Format("Certificate Name:'{0}' not found", certName));
            if (cert.Status == EntityStatus.Deleted)
                throw new CertificateStorageException(string.Format("Certificate Name:{0} was deleted", certName));

            return ConvertTo(cert);
        }

        private CertificateInfo ConvertTo(Certificate cert)
        {
            var certInfo = new CertificateInfo() { CertificateId = cert.CertificateId, Name = cert.Name };
            string psw = Crypto.DecryptString(cert.Password, _config.SecurityKey, SecurityVector);
            certInfo.Password = psw.ConvertToSecureString();
            string certFilePath = _fileStorageService.GetStorageItemPath(cert.FileId);
            certInfo.CertificateFile = new FileStream(certFilePath, FileMode.Open, FileAccess.Read);
            return certInfo;
        }

        public void Update(CertificateInfo certInfo)
        {
            if (certInfo == null)
                throw new ArgumentNullException("certInfo");

            Certificate cert = _certRepository.Find(certInfo.CertificateId);
            if (cert == null)
                throw new CertificateStorageException(string.Format("Certificate ID:{0} not found", certInfo.CertificateId));
            if (cert.Status == EntityStatus.Deleted)
                throw new CertificateStorageException(string.Format("Certificate ID:{0} was deleted", certInfo.CertificateId));

            cert.Name = certInfo.Name;
            string psw = certInfo.Password.ConvertToUnsecureString();
            cert.Password = Crypto.EncryptString(psw, _config.SecurityKey, SecurityVector);

            int oldFileId = cert.FileId;
            if (certInfo.CertificateFile != null)
                cert.FileId = _fileStorageService.Put(certInfo.CertificateFile);

            _certRepository.Update(cert);
            _unitOfWork.Save();

            if (certInfo.CertificateFile != null)
                _fileStorageService.DeleteStorageItem(oldFileId);
        }

        public void Delete(int certId)
        {
            Certificate cert = _certRepository.Query()
                .Filter(x => x.CertificateId == certId && x.Status != EntityStatus.Deleted)
                .Get().FirstOrDefault();
            if (cert == null)
                return;

            cert.Status = EntityStatus.Deleted;
            _certRepository.Update(cert);
            _unitOfWork.Save();
        }

        #region IDisposable
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
        #endregion
    }
}
