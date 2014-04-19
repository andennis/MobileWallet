using CertificateStorage.Core;
using Common.Configuration;

namespace CertificateStorage.BL
{
    public class CertificateStorageConfig : AppDbConfig, ICertificateStorageConfig
    {
        public CertificateStorageConfig()
            : base("CertificateStorage")
        {
        }

        public string SecurityKey
        {
            get { return GetValue("SecurityKey"); }
        }
    }
}
