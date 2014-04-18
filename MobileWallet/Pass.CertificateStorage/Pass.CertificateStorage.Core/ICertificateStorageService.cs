using System;
using Pass.CertificateStorage.Core.Entities;

namespace Pass.CertificateStorage.Core
{
    public interface ICertificateStorageService : IDisposable
    {
        int Put(CertificateInfo certInfo);
        CertificateInfo Read(int certId);
        void Update(int certId, CertificateInfo certInfo);
        void Delete(int certId);
    }
}
