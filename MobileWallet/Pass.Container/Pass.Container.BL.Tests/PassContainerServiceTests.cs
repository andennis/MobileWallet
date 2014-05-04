using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CertificateStorage.Core;
using CertificateStorage.Core.Entities;
using FileStorage.BL;
using Moq;
using NUnit.Framework;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Factory;
using Pass.Container.Repository.Core.Entities;


namespace Pass.Container.BL.Tests
{
    [TestFixture]
    public class PassContainerServiceTests
    {
        private readonly FileStorageConfig _fsConfig;

        public PassContainerServiceTests()
        {
            _fsConfig = new FileStorageConfig();
        }

        [TestFixtureSetUp]
        public void InitAllTests()
        {
            TestHelper.ClearFileStorage(_fsConfig);
        }

        [Test]
        public void DisposeTest()
        {
            IPassContainerService pcs = GetPassContainerService();
            Assert.IsInstanceOf<IDisposable>(pcs);
            Assert.DoesNotThrow(pcs.Dispose);
        }

        [Test]
        public void CreatePassTest()
        {
            using (var pts = GetPassTemplateService())
            using (var pcs = GetPassContainerService())
            {
                string testPassTemplateDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestPassTemplate");
                TestHelper.PreparePassTemplateSource(testPassTemplateDir, "template.xml");
                int passTemplateId = pts.CreatePassTemlate(testPassTemplateDir);

                IList<PassFieldInfo> fields = pts.GetPassTemplateFields(passTemplateId);
                int passId = pcs.CreatePass(passTemplateId, fields);
                Assert.Greater(passId, 0);
                //TODO not finished
            }
        }

        [Test]
        public void UpdatePassFieldsTest()
        {
            throw new NotImplementedException();
        }

        private IPassContainerService GetPassContainerService()
        {
            return PassContainerFactory.CreateContainerService(new PassContainerConfig());
        }

        private IPassTemplateService GetPassTemplateService()
        {
            return PassContainerFactory.CreateTemplateService(GetMockCertificateStorageService(), GetMockPassCertificateService());
        }

        private ICertificateStorageService GetMockCertificateStorageService()
        {
            var scService = new Mock<ICertificateStorageService>();
            var cert = new CertificateInfo() {CertificateId = 1, Name = "Cert1"};
            scService.Setup(x => x.Read(It.Is<string>(v => v == "TID#YHQB764QFA/PTID#pass.com.passlight.dev.test"))).Returns(cert);
            return scService.Object;
        }
        private IPassCertificateService GetMockPassCertificateService()
        {
            var certService = new Mock<IPassCertificateService>();
            certService.Setup(x => x.GetCertificate(It.Is<int>(v => v == 1))).Returns(TestHelper.GetCertificateApple());
            return certService.Object;
        }


    }
}
