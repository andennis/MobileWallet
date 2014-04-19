using Common.Repository;

namespace CertificateStorage.Core
{
    public interface ICertificateStorageConfig : IDbConfig
    {
        string SecurityKey { get; }
    }
}