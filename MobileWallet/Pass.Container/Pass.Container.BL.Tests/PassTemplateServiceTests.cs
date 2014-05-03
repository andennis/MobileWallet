using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Extensions;
using Common.Repository;
using Common.Utils;
using FileStorage.BL;
using FileStorage.Core;
using FileStorage.Factory;
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
        private const string TemplateFileName = "template.xml";
        private const string TempFolder = @"Data\TemplateService\Temp";

        //UnitOfWork
        private IPassContainerUnitOfWork _pcUnitOfWork;
        private readonly string _testPassTemplateDir;
        private readonly string _passTemplateFileName;

        //Repositories
        private IRepository<PassTemplate> _repPassTemplate;
        private IRepository<PassTemplateApple> _repPassTemplateApple;
        private IRepository<PassField> _repPassField;

        public PassTemplateServiceTests()
        {
            _testPassTemplateDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestPassTemplate");
            _passTemplateFileName = "template.xml";
        }

        [SetUp]
        public virtual void InitEachTest()
        {
            _pcUnitOfWork = TestHelper.PassContainerUnitOfWork;

            //Repositories
            _repPassTemplate = _pcUnitOfWork.GetRepository<PassTemplate>();
            _repPassTemplateApple = _pcUnitOfWork.GetRepository<PassTemplateApple>();
            _repPassField = _pcUnitOfWork.GetRepository<PassField>();

            //Clear temp folder
            if (Directory.Exists(TempFolder))
            {
                Directory.Delete(TempFolder, true);
            }
            Directory.CreateDirectory(TempFolder);
        }

        [TearDown]
        public virtual void FinalizeEachTest()
        {
            _pcUnitOfWork.Dispose();
        }

        [Test]
        public void PassTemplateXmlSerializationTest()
        {
            GeneralPassTemplate generalTemplate = TestHelper.GetPassTemplateObject();
            string path = Path.Combine(_testPassTemplateDir, "Template.xml");
            if (File.Exists(path))
                File.Delete(path);
            generalTemplate.SaveToXml(path);
            Assert.IsTrue(File.Exists(path));

            GeneralPassTemplate template;
            template = path.LoadFromXml<GeneralPassTemplate>();
            Assert.AreEqual(generalTemplate.TemplateName, template.TemplateName);
        }

        [Test]
        public void PassTemplateJsonSerializationTest()
        {
            GeneralPassTemplate generalTemplate = TestHelper.GetPassTemplateObject();
            string path = Path.Combine(_testPassTemplateDir, "Template.json");
            if (File.Exists(path))
                File.Delete(path);
            string json = generalTemplate.ObjectToJson();
            File.WriteAllText(path, json);
            Assert.IsTrue(File.Exists(path));

            string jsonResult = File.ReadAllText(path);
            GeneralPassTemplate template = jsonResult.JsonToObject<GeneralPassTemplate>();
            Assert.AreEqual(generalTemplate.TemplateName, template.TemplateName);
        }

        [Test]
        public void CreatePassTemplateTest()
        {
            int passTemplateId;
            using (IPassTemplateService passTemplateService = GetPassTemplateService())
            {
                Assert.Throws<ArgumentNullException>(() => passTemplateService.CreatePassTemlate(TemplateFolder));
                passTemplateId = passTemplateService.CreatePassTemlate(TemplateFolder);
                Assert.Greater(passTemplateId, 0);
            }

            string templateFilePath = Path.Combine(TemplateFolder, TemplateFileName);
            var generalPassTemplate = templateFilePath.LoadFromXml<GeneralPassTemplate>();
            
            //Check pass template DB record
            PassTemplate passTemplate = _repPassTemplate.Query()
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
            Assert.AreEqual(generalPassTemplate.PassTypeIdentifier, applePassTemplate.CertificateId);
            Assert.AreEqual(ClientDeviceType.Apple, applePassTemplate.ClientType);
            Assert.AreEqual(generalPassTemplate.PassTypeIdentifier, applePassTemplate.PassTypeId);

            //Check pass dynamic fields
            Assert.NotNull(passTemplate.PassFields);
            PassField[] passFields = passTemplate.PassFields.ToArray();
            Assert.AreEqual(1, passFields.Length);
            Assert.AreEqual("Key01", passFields[0].Name);
            Assert.AreEqual("LKey01", passFields[0].DefaultLabel);
            Assert.AreEqual("VKey01", passFields[0].DefaultValue);
            Assert.AreEqual(EntityStatus.Active, passFields[0].Status);
            
            //Check template storage
            IPassTemplateStorageService ptsService = GetPassTemplateStorageService();

            //General template
            string templatePath = Path.Combine(TempFolder, "General");
            ptsService.GetBaseTemplateFiles(passTemplate.PackageId, templatePath);
            Assert.True(File.Exists(Path.Combine(templatePath, TemplateFileName)));

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
                //Assert.AreEqual("???", passFields[0].DefaultLabel);
                //Assert.AreEqual("???", passFields[0].DefaultValue);
            }
        }

        [Test]
        public void UpdatePassTemplateTest()
        {
            string newTemplateFolder = Path.Combine(TempFolder, "Template2");
            FileHelper.DirectoryCopy(TemplateFolder, newTemplateFolder, true);

            using (var passTemplateService = GetPassTemplateService())
            {
                int passTemplateId = passTemplateService.CreatePassTemlate(TemplateFolder);
                PassTemplate passTemplate1 = _repPassTemplate.Query()
                    .Include(x => x.NativeTemplates)
                    .Include(x => x.PassFields)
                    .Filter(x => x.PassTemplateId == passTemplateId)
                    .Get().First();

                string newTemplateFile = Path.Combine(newTemplateFolder, TemplateFileName);
                var generalPassTemplate = newTemplateFile.LoadFromXml<GeneralPassTemplate>();

                generalPassTemplate.TemplateName = "Template2";
                generalPassTemplate.CertificateId = 222;
                generalPassTemplate.PassTypeIdentifier = "NewID";
                GeneralField fld = generalPassTemplate.FieldDetails.BackFields.First(x => x.Key == "Key01");
                fld.Label = "LKey01_new";
                fld.Value = "VKey01_new";
                generalPassTemplate.FieldDetails.BackFields.Add(new GeneralField(){Key = "Key02", Label = "LKey02", Value = "VKey02", IsDynamicValue = true, Type = GeneralField.DataType.Text});

                generalPassTemplate.SaveToXml(newTemplateFile);
                passTemplateService.UpdatePassTemlate(passTemplateId, newTemplateFolder);

                //Check pass template DB record
                PassTemplate passTemplate2 = _repPassTemplate.Query()
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
                PassTemplateApple applePassTemplate = passTemplate2.NativeTemplates.OfType<PassTemplateApple>().FirstOrDefault();
                Assert.NotNull(applePassTemplate);
                Assert.AreEqual(generalPassTemplate.CertificateId, applePassTemplate.CertificateId);
                Assert.AreEqual(ClientDeviceType.Apple, applePassTemplate.ClientType);
                Assert.AreEqual(generalPassTemplate.PassTypeIdentifier, applePassTemplate.PassTypeId);

                //Check pass dynamic fields
                Assert.NotNull(passTemplate2.PassFields);
                PassField[] passFields = passTemplate2.PassFields.ToArray();
                Assert.AreEqual(2, passFields.Length);

                PassField pf = passFields.FirstOrDefault(x => x.Name == "Key01");
                Assert.IsNotNull(pf);
                Assert.AreEqual("LKey01_new", pf.DefaultLabel);
                Assert.AreEqual("VKey01_new", pf.DefaultValue);
                Assert.AreEqual(EntityStatus.Active, pf.Status);

                pf = passFields.FirstOrDefault(x => x.Name == "Key02");
                Assert.IsNotNull(pf);
                Assert.AreEqual("LKey02", pf.DefaultLabel);
                Assert.AreEqual("VKey02", pf.DefaultValue);
                Assert.AreEqual(EntityStatus.Active, pf.Status);
            }
        }

        [Test]
        public void DeletePassTemplateTest()
        {
            int passTemplateId;
            using (var passTemplateService = GetPassTemplateService())
            {
                TestHelper.PreparePassTemplateSource(_testPassTemplateDir, _passTemplateFileName);
                passTemplateId = passTemplateService.CreatePassTemlate(_testPassTemplateDir);
                Assert.Greater(passTemplateId, 0);
            }

            PassTemplate passTemplate = _repPassTemplate.Find(passTemplateId);
            Assert.IsNotNull(passTemplate);
            Assert.AreEqual(EntityStatus.Active, passTemplate.Status);

            //Check file storage
            string storageItemPath;
            using (var fsService = GetFileStorageService())
            {
                int packageId = passTemplate.PackageId;
                storageItemPath = fsService.GetStorageItemPath(packageId);
            }
            Assert.IsNotNull(storageItemPath);
            string[] directories = Directory.GetDirectories(storageItemPath);
            Assert.AreEqual(2, directories.Count());
            foreach (string dir in directories)
            {
                Assert.IsTrue(
                    Directory.GetFiles(dir)
                             .Any(x => (Path.GetExtension(x) == ".json") || (Path.GetExtension(x) == ".xml")));
            }
            using (var passTemplateService = GetPassTemplateService())
            {
                passTemplateService.DeletePassTemplate(passTemplateId);
            }

            //PassTemplate passTemplateAfterDelete = _repPassTemplate.Find(passTemplateId);
            //Assert.IsNotNull(passTemplateAfterDelete);
            //Assert.AreEqual(TemplateStatus.InActive, passTemplateAfterDelete.Status);

            //Check file storage
            directories = Directory.GetDirectories(storageItemPath);
            Assert.AreEqual(2, directories.Count());
            foreach (string dir in directories)
            {
                Assert.IsTrue(
                    Directory.GetFiles(dir)
                             .Any(x => (Path.GetExtension(x) == ".json") || (Path.GetExtension(x) == ".xml")));
            }
            ////Check file storage item as Deleted
            //packageId = passTemplate.PackageId;
            //storageItemPath = _fsService.GetStorageItemPath(packageId);
            //Assert.IsNull(storageItemPath);

        }

        private IPassTemplateService GetPassTemplateService()
        {
            return PassContainerFactory.CreateTemplateService(new PassContainerConfig(), new FileStorageConfig());
        }
        private IPassTemplateStorageService GetPassTemplateStorageService()
        {
            return PassContainerFactory.CreatePassTemplateStorageService();
        }
        private IFileStorageService GetFileStorageService()
        {
            return FileStorageFactory.Create(new FileStorageConfig());
        }
    }
}
