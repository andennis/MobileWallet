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
using Pass.Container.Repository.Core;
using Pass.Container.Repository.Core.Entities;
using Pass.Container.Repository.EF;


namespace Pass.Container.BL.Tests
{
    [TestFixture]
    public class PassContainerServiceTests
    {
        private const string TemplateFolder = @"Data\ContainerService\Template";
        private const string TempFolder = @"Data\ContainerService\Temp";

        [SetUp]
        public virtual void InitEachTest()
        {
            //Clear temp folder
            if (Directory.Exists(TempFolder))
            {
                Directory.Delete(TempFolder, true);
            }
            Directory.CreateDirectory(TempFolder);
        }

        [Test]
        public void CreatePassAndCheckFieldsTest()
        {
            using (var pts = GetPassTemplateService())
            using (var pcs = GetPassContainerService())
            {
                int passTemplateId = pts.CreatePassTemlate(TemplateFolder);

                IList<PassFieldInfo> fields1 = new List<PassFieldInfo>()
                                                  {
                                                      new PassFieldInfo()
                                                          {
                                                              Name = "Key01", 
                                                              Label = Guid.NewGuid().ToString(),
                                                              Value = Guid.NewGuid().ToString()
                                                          },
                                                      new PassFieldInfo()
                                                          {
                                                              Name = "Key02", 
                                                              Label = Guid.NewGuid().ToString(),
                                                          },
                                                      new PassFieldInfo()
                                                          {
                                                              Name = "Key03", 
                                                              Value = Guid.NewGuid().ToString()
                                                          }
                                                  };
                int passId = pcs.CreatePass(passTemplateId, fields1);
                Assert.Greater(passId, 0);

                IList<PassFieldInfo> fields2 = pcs.GetPassFields(passId);
                Assert.AreEqual(4, fields2.Count);

                PassFieldInfo field1 = fields1.First(x => x.Name == "Key01");
                PassFieldInfo field2 = fields2.FirstOrDefault(x => x.Name == field1.Name);
                Assert.NotNull(field2);
                Assert.AreEqual(field1.Label, field2.Label);
                Assert.AreEqual(field1.Value, field2.Value);

                field1 = fields1.First(x => x.Name == "Key02");
                field2 = fields2.FirstOrDefault(x => x.Name == field1.Name);
                Assert.NotNull(field2);
                Assert.AreEqual(field1.Label, field2.Label);
                Assert.AreEqual("VKey02", field2.Value);

                field1 = fields1.First(x => x.Name == "Key03");
                field2 = fields2.FirstOrDefault(x => x.Name == field1.Name);
                Assert.NotNull(field2);
                Assert.AreEqual("LKey03", field2.Label);
                Assert.AreEqual(field1.Value, field2.Value);

                field2 = fields2.FirstOrDefault(x => x.Name == "Key04");
                Assert.NotNull(field2);
                Assert.AreEqual("LKey04", field2.Label);
                Assert.AreEqual("VKey04", field2.Value);
            }
        }

        [Test]
        public void UpdatePassAndCheckFieldsTest()
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
