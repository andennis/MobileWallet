using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileStorage.Core;
using Pass.CertificateStorage.Core;
using Pass.CertificateStorage.Core.Entities;

namespace Pass.CertificateStorage.BL
{
    public class CertificateStorageService : ICertificateStorageService
    {
        private readonly ICertificateStorageConfig _config;
        private readonly IFileStorageService _fileStorageService;

        public CertificateStorageService(ICertificateStorageConfig config, IFileStorageService fileStorageService)
        {
            _config = config;
            _fileStorageService = fileStorageService;
        }

        public int Put(CertificateInfo certInfo)
        {
            throw new NotImplementedException();
        }

        public CertificateInfo Read(int certId)
        {
            throw new NotImplementedException();
        }

        public int Update(int certId, CertificateInfo certInfo)
        {
            throw new NotImplementedException();
        }
    }
}
