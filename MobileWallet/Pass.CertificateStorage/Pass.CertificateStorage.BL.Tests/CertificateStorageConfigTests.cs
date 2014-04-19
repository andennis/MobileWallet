using Common.Repository;
using NUnit.Framework;
using Pass.CertificateStorage.Core;

namespace Pass.CertificateStorage.BL.Tests
{
    [TestFixture]
    public class CertificateStorageConfigTests
    {
        [Test]
        public void CertificateStorageConfigTest()
        {
            var fsConfig = new CertificateStorageConfig();
            Assert.AreEqual("1234567890987654", fsConfig.SecurityKey);
            Assert.IsNotNullOrEmpty(fsConfig.ConnectionString);
        }

        [Test]
        public void CertificateStorageConfigInterfacesTest()
        {
            var fsConfig = new CertificateStorageConfig();
            Assert.IsInstanceOf<ICertificateStorageConfig>(fsConfig);
            Assert.IsInstanceOf<IDbConfig>(fsConfig);
        }
    }
}
