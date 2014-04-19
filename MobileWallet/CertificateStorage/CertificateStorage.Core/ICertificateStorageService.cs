using System;
using CertificateStorage.Core.Entities;

namespace CertificateStorage.Core
{
    public interface ICertificateStorageService : IDisposable
    {
        int Put(CertificateInfo certInfo);
        CertificateInfo Read(int certId);
        void Update(int certId, CertificateInfo certInfo);
        void Delete(int certId);
    }
}
