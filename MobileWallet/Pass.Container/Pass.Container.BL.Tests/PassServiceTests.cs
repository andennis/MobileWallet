using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CertificateStorage.Core;
using CertificateStorage.Core.Entities;
using Moq;
using NUnit.Framework;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Core.Exceptions;
using Pass.Container.Factory;


namespace Pass.Container.BL.Tests
{
    [TestFixture]
    public class PassServiceTests
    {
        private const string TemplateFolder = @"Data\ContainerService\Template";
        private const string TempFolder = @"Data\ContainerService\Temp";
        private int _passTemplateId;

        [TestFixtureSetUp]
        public void InitAllTests()
        {
            using (var pts = GetPassTemplateService())
            {
                _passTemplateId = pts.CreatePassTemlate(TemplateFolder);
            }
        }

        [SetUp]
        public void InitEachTest()
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
            int passId;
            //Create pass
            using (IPassService pcs = GetPassContainerService())
            {
                passId = pcs.CreatePass(_passTemplateId, fields1);
                Assert.Greater(passId, 0);
            }

            //Check pass fields
            using (IPassService pcs = GetPassContainerService())
            {
                IList<PassFieldInfo> fields2 = pcs.GetPassFields(passId);
                Assert.NotNull(fields2);
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
        public void UpdatePassFieldsTest()
        {
            int passId;
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
            //Create pass and update pass fields
            using (IPassService pcs = GetPassContainerService())
            {
                //Create pass
                passId = pcs.CreatePass(_passTemplateId, fields1);
                Assert.Greater(passId, 0);

                IList<PassFieldInfo> fields2 = pcs.GetPassFields(passId);
                Assert.NotNull(fields2);
                Assert.AreEqual(4, fields2.Count);

                //Update pass fields
                PassFieldInfo field1 = fields1.First(x => x.Name == "Key01");
                field1.Label = Guid.NewGuid().ToString();
                field1.Value = Guid.NewGuid().ToString();

                field1 = fields1.First(x => x.Name == "Key02");
                field1.Label = null;
                field1.Value = Guid.NewGuid().ToString();

                field1 = fields1.First(x => x.Name == "Key03");
                field1.Label = Guid.NewGuid().ToString();
                field1.Value = null;

                Assert.DoesNotThrow(() => pcs.UpdatePassFields(passId, fields1));

                var wrongFields = new List<PassFieldInfo>() {new PassFieldInfo() {Name = Guid.NewGuid().ToString()}};
                Assert.Throws<PassContainerException>(() => pcs.UpdatePassFields(passId, wrongFields));
                Assert.Throws<PassContainerException>(() => pcs.UpdatePassFields(-1, wrongFields));
                Assert.Throws<ArgumentNullException>(() => pcs.UpdatePassFields(passId, null));
            }

            //Check pass fields
            using (IPassService pcs = GetPassContainerService())
            {
                IList<PassFieldInfo> fields2 = pcs.GetPassFields(passId);
                Assert.NotNull(fields2);
                Assert.AreEqual(4, fields2.Count);

                PassFieldInfo field1 = fields1.First(x => x.Name == "Key01");
                PassFieldInfo field2 = fields2.FirstOrDefault(x => x.Name == field1.Name);
                Assert.NotNull(field2);
                Assert.AreEqual(field1.Label, field2.Label);
                Assert.AreEqual(field1.Value, field2.Value);

                field1 = fields1.First(x => x.Name == "Key02");
                field2 = fields2.FirstOrDefault(x => x.Name == field1.Name);
                Assert.NotNull(field2);
                Assert.AreEqual("LKey02", field2.Label);
                Assert.AreEqual(field1.Value, field2.Value);

                field1 = fields1.First(x => x.Name == "Key03");
                field2 = fields2.FirstOrDefault(x => x.Name == field1.Name);
                Assert.NotNull(field2);
                Assert.AreEqual(field1.Label, field2.Label);
                Assert.AreEqual("VKey03", field2.Value);

                field2 = fields2.FirstOrDefault(x => x.Name == "Key04");
                Assert.NotNull(field2);
                Assert.AreEqual("LKey04", field2.Label);
                Assert.AreEqual("VKey04", field2.Value);
            }
        }

        [Test]
        public void GetPassPackageAppleTest()
        {
            int passId;
            using (IPassService pcs = GetPassContainerService())
            {
                IList<PassFieldInfo> fields1 = new List<PassFieldInfo>()
                                                   {
                                                       new PassFieldInfo()
                                                           {
                                                               Name = "Key01",
                                                               Label = "MyL1",
                                                               Value = "MyV1"
                                                           }
                                                   };

                passId = pcs.CreatePass(_passTemplateId, fields1);
            }

            using (IPassService pcs = GetPassContainerService())
            {
                string fileName = pcs.GetPassPackage(passId, ClientType.Apple);
                Assert.IsNotNullOrEmpty(fileName);
                Assert.True(File.Exists(fileName));
            }
        }

        private IPassService GetPassContainerService()
        {
            return PassContainerFactory.CreateContainerService(GetMockCertificateStorageService(), GetMockPassCertificateService());
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
