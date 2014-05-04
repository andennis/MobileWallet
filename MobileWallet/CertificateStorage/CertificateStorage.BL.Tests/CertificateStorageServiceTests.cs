using System;
using System.IO;
using CertificateStorage.Core;
using CertificateStorage.Core.Entities;
using CertificateStorage.Repository.EF;
using Common.Extensions;
using FileStorage.BL;
using FileStorage.Repository.EF;
using NUnit.Framework;

namespace CertificateStorage.BL.Tests
{
    [TestFixture]
    public class CertificateStorageServiceTests
    {
        private const string TestFile1 = @"Data\TextFile1.txt";
        private const string TestFile2 = @"Data\TextFile2.txt";

        private ICertificateStorageService _certService;

        [TestFixtureSetUp]
        public void InitAllTests()
        {
            _certService = GetCertificateStorageService();
        }

        [Test]
        public void PutAndReadTest()
        {
            Assert.Throws<ArgumentNullException>(() => _certService.Put(null));
            int certId;
            const string psw = "Psw1";

            //Put
            var cert = new CertificateInfo() { Name = Guid.NewGuid().ToString(), Password = psw.ConvertToSecureString() };
            Assert.Throws<CertificateStorageException>(() => _certService.Put(cert));

            using (var fs = new FileStream(TestFile1, FileMode.Open, FileAccess.Read))
            {
                cert.CertificateFile = fs;
                certId = _certService.Put(cert);
                Assert.Greater(certId, 0);
            }

            //Read by ID
            using (CertificateInfo cert2 = _certService.Read(certId))
            {
                Assert.IsNotNull(cert2);
                Assert.AreEqual(cert.Name, cert2.Name);
                Assert.AreEqual(psw, cert2.Password.ConvertToUnsecureString());
                Assert.IsNotNull(cert2.CertificateFile);

                var sr = new StreamReader(cert2.CertificateFile);
                string fileContent = sr.ReadToEnd();
                Assert.AreEqual("Hello!!!", fileContent);
            }

            //Read by name
            using (CertificateInfo cert3 = _certService.Read(cert.Name))
            {
                Assert.IsNotNull(cert3);
                Assert.AreEqual(cert.Name, cert3.Name);
                Assert.AreEqual(psw, cert3.Password.ConvertToUnsecureString());
                Assert.IsNotNull(cert3.CertificateFile);

                var sr = new StreamReader(cert3.CertificateFile);
                string fileContent = sr.ReadToEnd();
                Assert.AreEqual("Hello!!!", fileContent);
            }

            Assert.Throws<CertificateStorageException>(() => _certService.Read(-1));
        }

        [Test]
        public void UpdateTest()
        {
            Assert.Throws<ArgumentNullException>(() => _certService.Update(null));
            Assert.Throws<CertificateStorageException>(() => _certService.Update(new CertificateInfo()));

            int certId = PutCertificate(Guid.NewGuid().ToString(), "123", TestFile1);

            const string psw = "321";
            var cert1 = new CertificateInfo()
                        {
                            CertificateId = certId, 
                            Name = Guid.NewGuid().ToString(), 
                            Password = psw.ConvertToSecureString()
                        };

            //Update only password and name
            Assert.DoesNotThrow(() => _certService.Update(cert1));
            using (CertificateInfo cert2 = _certService.Read(certId))
            {
                Assert.IsNotNull(cert2);
                Assert.AreEqual(cert1.Name, cert2.Name);
                Assert.AreEqual(psw, cert2.Password.ConvertToUnsecureString());
                Assert.IsNotNull(cert2.CertificateFile);

                var sr = new StreamReader(cert2.CertificateFile);
                string fileContent = sr.ReadToEnd();
                Assert.AreEqual("Hello!!!", fileContent);
            }

            //Update file
            using (var fs = new FileStream(TestFile2, FileMode.Open, FileAccess.Read))
            {
                cert1.CertificateFile = fs;
                Assert.DoesNotThrow(() => _certService.Update(cert1));
            }
            using (CertificateInfo cert2 = _certService.Read(certId))
            {
                Assert.IsNotNull(cert2);
                Assert.AreEqual(cert1.Name, cert2.Name);
                Assert.AreEqual(psw, cert2.Password.ConvertToUnsecureString());
                Assert.IsNotNull(cert2.CertificateFile);

                var sr = new StreamReader(cert2.CertificateFile);
                string fileContent = sr.ReadToEnd();
                Assert.AreEqual("World!!!", fileContent);
            }
        }

        [Test]
        public void DeleteTest()
        {
            int certId = PutCertificate(Guid.NewGuid().ToString(), "123", TestFile1);
            Assert.DoesNotThrow(() => _certService.Read(certId));

            _certService.Delete(certId);
            Assert.Throws<CertificateStorageException>(() => _certService.Read(certId));
        }

        private int PutCertificate(string name, string password, string filePath)
        {
            var cert = new CertificateInfo() { Name = name, Password = password.ConvertToSecureString() };
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                cert.CertificateFile = fs;
                return _certService.Put(cert);
            }
        }

        private ICertificateStorageService GetCertificateStorageService()
        {
            var cerConfig = new CertificateStorageConfig();
            var fsConfig = new FileStorageConfig();
            return new CertificateStorageService(cerConfig,
                new CertificateStorageUnitOfWork(cerConfig),
                new FileStorageService(fsConfig, new FileStorageUnitOfWork(fsConfig)));
        }

    }
}
