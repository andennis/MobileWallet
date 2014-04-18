using Common.Repository;

namespace Pass.CertificateStorage.Core
{
    public interface ICertificateStorageConfig : IDbConfig
    {
        string SecurityKey { get; }
    }
}