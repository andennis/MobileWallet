using Pass.CertificateStorage.Core.Entities;

namespace Pass.CertificateStorage.Core
{
    public interface ICertificateStorageService
    {
        int Put(CertificateInfo certInfo);
        CertificateInfo Read(int certId);
        int Update(int certId, CertificateInfo certInfo);
    }
}
