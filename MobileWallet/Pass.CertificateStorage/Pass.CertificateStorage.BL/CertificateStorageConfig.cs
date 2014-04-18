using Common.Configuration;
using Pass.CertificateStorage.Core;

namespace Pass.CertificateStorage.BL
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
