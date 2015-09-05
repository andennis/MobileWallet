using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CertificateStorage.Core;
using CertificateStorage.Core.Entities;
using Common.Extensions;
using Common.Repository;
using Common.Utils;
using Moq;
using NUnit.Framework;
using Pass.Container.BL.Helpers;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Core.Entities.Templates.GeneralPassTemplate;
using Pass.Container.Factory;
using Pass.Container.Repository.Core;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.BL.Tests
{
    [TestFixture]
    public class PassTemplateServiceTests
    {
        private const string TemplateFolder = @"Data\TemplateService\Template";
        private const string TempFolder = @"Data\TemplateService\Temp";

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
        public void PassTemplateXmlSerializationTest()
        {
            string templatePath = Path.Combine(TemplateFolder, TestHelper.TemplateFileName);
            var generalTemplate1 = templatePath.LoadFromXml<GeneralPassTemplate>();
            generalTemplate1.TemplateName = Guid.NewGuid().ToString();

            templatePath = Path.Combine(TempFolder, TestHelper.TemplateFileName);
            generalTemplate1.SaveToXml(templatePath);

            var generalTemplate2 = templatePath.LoadFromXml<GeneralPassTemplate>();
            Assert.AreEqual(generalTemplate1.TemplateName, generalTemplate2.TemplateName);
        }

        [Test]
        public void CreatePassTemplateTest()
        {
            int passTemplateId;
            using (IPassTemplateService passTemplateService = GetPassTemplateService())
            {
                Assert.Throws<ArgumentNullException>(() => passTemplateService.CreatePassTemlate(null));
                passTemplateId = passTemplateService.CreatePassTemlate(TemplateFolder);
                Assert.Greater(passTemplateId, 0);
            }

            string templateFilePath = Path.Combine(TemplateFolder, TestHelper.TemplateFileName);
            var generalPassTemplate = templateFilePath.LoadFromXml<GeneralPassTemplate>();

            using (IPassContainerUnitOfWork pcUnitOfWork = TestHelper.PassContainerUnitOfWork)
            {
                //Check pass template DB record
                PassTemplate passTemplate = pcUnitOfWork.GetRepository<PassTemplate>()
                    .Query()
                    .Include(x => x.NativeTemplates)
                    .Include(x => x.PassFields)
                    .Filter(x => x.PassTemplateId == passTemplateId)
                    .Get().FirstOrDefault();
                Assert.IsNotNull(passTemplate);
                Assert.Greater(passTemplate.PackageId, 0);
                Assert.AreEqual(generalPassTemplate.TemplateName, passTemplate.Name);
                Assert.AreEqual(EntityStatus.Active, passTemplate.Status);

                //Check native pass templates
                Assert.NotNull(passTemplate.NativeTemplates);
                PassTemplateApple applePassTemplate = passTemplate.NativeTemplates.OfType<PassTemplateApple>().FirstOrDefault();
                Assert.NotNull(applePassTemplate);
                Assert.AreEqual(ClientType.Apple, applePassTemplate.DeviceType);
                Assert.AreEqual(generalPassTemplate.PassTypeIdentifier, applePassTemplate.PassTypeId);

                //Check pass dynamic fields
                Assert.NotNull(passTemplate.PassFields);
                PassField[] passFields = passTemplate.PassFields.ToArray();
                Assert.AreEqual(1, passFields.Length);
                Assert.AreEqual("Key01", passFields[0].Name);
                //Assert.AreEqual("LKey01", passFields[0].DefaultLabel);
                //Assert.AreEqual("VKey01", passFields[0].DefaultValue);
                Assert.AreEqual(EntityStatus.Active, passFields[0].Status);

                //Check template storage
                IPassTemplateStorageService ptsService = GetPassTemplateStorageService();

                //General template
                string templatePath = Path.Combine(TempFolder, "General");
                ptsService.GetBaseTemplateFiles(passTemplate.PackageId, templatePath);
                Assert.True(File.Exists(Path.Combine(templatePath, TestHelper.TemplateFileName)));

                string dstImageFolderPath = Path.Combine(templatePath, "Images");
                Assert.True(Directory.Exists(dstImageFolderPath));
                Assert.True(File.Exists(Path.Combine(dstImageFolderPath, "icon.png")));

                //Apple template
                templatePath = Path.Combine(TempFolder, "Apple");
                ptsService.GetNativeTemplateFiles(passTemplate.PackageId, ClientType.Apple, templatePath);
                Assert.True(File.Exists(Path.Combine(templatePath, ApplePass.PassTemplateFileName)));
                Assert.True(File.Exists(Path.Combine(templatePath, ApplePass.ManifestTemplateFileName)));

                dstImageFolderPath = Path.Combine(templatePath, ApplePass.TemplateImageFolder);
                Assert.True(Directory.Exists(dstImageFolderPath));
                Assert.True(File.Exists(Path.Combine(dstImageFolderPath, "icon.png")));
            }
        }

        [Test]
        public void GetPassFieldsTest()
        {
            using (var passTemplateService = GetPassTemplateService())
            {
                int passTemplateId = passTemplateService.CreatePassTemlate(TemplateFolder);
                IList<PassFieldInfo> passFields = passTemplateService.GetPassTemplateFields(passTemplateId);
                Assert.IsNotNull(passFields);
                Assert.AreEqual(1, passFields.Count);
                Assert.AreEqual("Key01", passFields[0].Name);
                //Assert.AreEqual("LKey01", passFields[0].Label);
                //Assert.AreEqual("VKey01", passFields[0].Value);
            }
        }

        [Test]
        public void UpdatePassTemplateTest()
        {
            int passTemplateId;
            PassTemplate passTemplate1;
            GeneralPassTemplate generalPassTemplate;
            using (var passTemplateService = GetPassTemplateService())
            using (IPassContainerUnitOfWork pcUnitOfWork = TestHelper.PassContainerUnitOfWork)
            {
                passTemplateId = passTemplateService.CreatePassTemlate(TemplateFolder);
                passTemplate1 = pcUnitOfWork.GetRepository<PassTemplate>()
                    .Query()
                    .Include(x => x.NativeTemplates)
                    .Include(x => x.PassFields)
                    .Filter(x => x.PassTemplateId == passTemplateId)
                    .Get().First();

                string newTemplateFolder = Path.Combine(TempFolder, "Template2");
                FileHelper.DirectoryCopy(TemplateFolder, newTemplateFolder, true);
                string newTemplateFile = Path.Combine(newTemplateFolder, TestHelper.TemplateFileName);
                generalPassTemplate = newTemplateFile.LoadFromXml<GeneralPassTemplate>();

                generalPassTemplate.TemplateName = "Template2";
                generalPassTemplate.PassTypeIdentifier = "NewID";
                GeneralField fld = generalPassTemplate.FieldDetails.BackFields.First(x => x.Key == "Key01");
                fld.Label = "LKey01_new";
                fld.Value = "VKey01_new";
                generalPassTemplate.FieldDetails.BackFields.Add(new GeneralField()
                                                                {
                                                                    Key = "Key02",
                                                                    Label = "LKey02",
                                                                    Value = "VKey02",
                                                                    IsDynamicValue = true,
                                                                    FieldType = GeneralField.DataType.Text
                                                                });

                generalPassTemplate.SaveToXml(newTemplateFile);
                passTemplateService.UpdatePassTemlate(passTemplateId, newTemplateFolder);
                Assert.Throws<ArgumentNullException>(() => passTemplateService.UpdatePassTemlate(passTemplateId, null));
            }

            using (IPassContainerUnitOfWork pcUnitOfWork = TestHelper.PassContainerUnitOfWork)
            {
                //Check pass template DB record
                PassTemplate passTemplate2 = pcUnitOfWork.GetRepository<PassTemplate>()
                    .Query()
                    .Include(x => x.NativeTemplates)
                    .Include(x => x.PassFields)
                    .Filter(x => x.PassTemplateId == passTemplateId)
                    .Get().FirstOrDefault();
                Assert.IsNotNull(passTemplate2);
                Assert.Greater(passTemplate2.PackageId, 0);
                Assert.AreNotEqual(passTemplate1.PackageId, passTemplate2.PackageId);
                Assert.AreEqual(generalPassTemplate.TemplateName, passTemplate2.Name);
                Assert.AreEqual(EntityStatus.Active, passTemplate2.Status);

                //Check native pass templates
                Assert.NotNull(passTemplate2.NativeTemplates);
                PassTemplateApple applePassTemplate =
                    passTemplate2.NativeTemplates.OfType<PassTemplateApple>().FirstOrDefault();
                Assert.NotNull(applePassTemplate);
                Assert.AreEqual(ClientType.Apple, applePassTemplate.DeviceType);
                Assert.AreEqual(generalPassTemplate.PassTypeIdentifier, applePassTemplate.PassTypeId);

                //Check pass dynamic fields
                Assert.NotNull(passTemplate2.PassFields);
                PassField[] passFields = passTemplate2.PassFields.ToArray();
                Assert.AreEqual(2, passFields.Length);

                PassField pf = passFields.FirstOrDefault(x => x.Name == "Key01");
                Assert.IsNotNull(pf);
                //Assert.AreEqual("LKey01_new", pf.DefaultLabel);
                //Assert.AreEqual("VKey01_new", pf.DefaultValue);
                Assert.AreEqual(EntityStatus.Active, pf.Status);

                pf = passFields.FirstOrDefault(x => x.Name == "Key02");
                Assert.IsNotNull(pf);
                //Assert.AreEqual("LKey02", pf.DefaultLabel);
                //Assert.AreEqual("VKey02", pf.DefaultValue);
                Assert.AreEqual(EntityStatus.Active, pf.Status);
            }
        }

        [Test]
        public void DeletePassTemplateTest()
        {
            int passTemplateId;
            using (IPassTemplateService templateService = GetPassTemplateService())
            {
                passTemplateId = templateService.CreatePassTemlate(TemplateFolder);
                Assert.Greater(passTemplateId, 0);
            }

            using (IPassContainerUnitOfWork pcUnitOfWork = TestHelper.PassContainerUnitOfWork)
            {
                PassTemplate passTemplate = pcUnitOfWork.GetRepository<PassTemplate>().Find(passTemplateId);
                Assert.IsNotNull(passTemplate);
                Assert.AreEqual(EntityStatus.Active, passTemplate.Status);
            }

            using (IPassTemplateService templateService = GetPassTemplateService())
            {
                templateService.DeletePassTemplate(passTemplateId);
            }

            using (IPassContainerUnitOfWork pcUnitOfWork = TestHelper.PassContainerUnitOfWork)
            {
                PassTemplate passTemplate = pcUnitOfWork.GetRepository<PassTemplate>().Find(passTemplateId);
                Assert.IsNotNull(passTemplate);
                Assert.AreEqual(EntityStatus.Deleted, passTemplate.Status);
            }
        }

        private IPassTemplateService GetPassTemplateService()
        {
            return PassContainerFactory.CreateTemplateService(GetMockCertificateStorageService(), null);
        }

        private ICertificateStorageService GetMockCertificateStorageService()
        {
            var scService = new Mock<ICertificateStorageService>();
            scService.Setup(x => x.Read(It.IsAny<string>())).Returns(new CertificateInfo() { CertificateId = 1, Name = "Cert1" });
            return scService.Object;
        }

        private IPassTemplateStorageService GetPassTemplateStorageService()
        {
            return PassContainerFactory.CreatePassTemplateStorageService();
        }
    }
}
