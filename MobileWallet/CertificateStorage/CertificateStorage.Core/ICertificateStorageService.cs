using System;
using CertificateStorage.Core.Entities;

namespace CertificateStorage.Core
{
    public interface ICertificateStorageService : IDisposable
    {
        int Put(CertificateInfo certInfo);
        CertificateInfo Read(int certId);
        CertificateInfo Read(string certName);
        void Update(CertificateInfo certInfo);
        void Delete(int certId);
    }
}
