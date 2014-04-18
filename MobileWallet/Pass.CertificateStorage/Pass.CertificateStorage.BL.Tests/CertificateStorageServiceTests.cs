using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Common.Extensions;
using FileStorage.BL;
using FileStorage.Repository.EF;
using NUnit.Framework;
using Pass.CertificateStorage.Core;
using Pass.CertificateStorage.Core.Entities;
using Pass.CertificateStorage.Repository.EF;

namespace Pass.CertificateStorage.BL.Tests
{
    [TestFixture]
    public class CertificateStorageServiceTests
    {
        private const string TestFile = @"Data\TextFile1.txt";

        [Test]
        public void PutTest()
        {
            using (var certService = GetCertificateStorageService())
            {
                var psw = "Psw1";
                var cert = new CertificateInfo() {Name = "N1", Password = psw.ConvertToSecureString()};
                using (var fs = new FileStream(TestFile, FileMode.Open, FileAccess.Read))
                {
                    cert.CerificateFile = fs;
                    int certId = certService.Put(cert);
                    Assert.Greater(certId, 0);
                }
            }
        }

        private ICertificateStorageService GetCertificateStorageService()
        {
            var cerConfig = new CertificateStorageConfig();
            var fsConfig = new FileStorageConfig();
            return new CertificateStorageService(cerConfig,
                new PassCertificateStorageUnitOfWork(cerConfig),
                new FileStorageService(fsConfig, new FileStorageUnitOfWork(fsConfig)));
        }

    }
}
