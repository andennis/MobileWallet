using System.IO;
using System.Security.Cryptography.X509Certificates;
using CertificateStorage.Core;
using CertificateStorage.Core.Entities;
using Common.Extensions;
using NUnit.Framework;
using Pass.Container.Core;
using Pass.Container.Factory;

namespace Pass.Container.BL.Tests
{
    [TestFixture]
    public class PassCertificateServiceTests
    {
        [Test]
        public void GetCertificateTest()
        {
            int certId;
            using (ICertificateStorageService certStorageService = PassContainerFactory.CreateCertificateStorageService())
            {
                var certInfo = new CertificateInfo() { Name = "pass.com.passlight.dev.test", Password = TestHelper.CertificateApplePassword.ConvertToSecureString() };
                using (var fs = new FileStream(TestHelper.CertificateFileApple, FileMode.Open, FileAccess.Read))
                {
                    certInfo.CertificateFile = fs;
                    certId = certStorageService.Put(certInfo);
                }
            }

            using (IPassCertificateService passCertificateService = PassContainerFactory.CreatePassCertificateService())
            {
                X509Certificate2 cert = passCertificateService.GetCertificate(certId);
                Assert.IsNotNull(cert);
                Assert.True(cert.HasPrivateKey);
                Assert.AreEqual("60EDD24B5968C7A7", cert.SerialNumber);
            }
        }
    }
}
