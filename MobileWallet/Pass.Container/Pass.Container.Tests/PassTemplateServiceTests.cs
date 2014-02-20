using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using Common.Extensions;
using Common.Repository;
using FileStorage.BL;
using FileStorage.Core;
using FileStorage.Factory;
using NUnit.Framework;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Core.Entities.Templates.GeneralPassTemplate;
using Pass.Container.Factory;
using Pass.Container.Repository.EF;

namespace Pass.Container.BL.Tests
{
    [TestFixture]
    public class PassTemplateServiceTests
    {
        //UnitOfWork
        private IPassContainerUnitOfWork _pcUnitOfWork;
        private readonly string _testPassTemplateDir;
        private readonly string _passTemplateFileName;

        //Repositories
        private IRepository<PassTemplate> _repPassTemplate;
        private IRepository<PassTemplateApple> _repPassTemplateApple;
        private IRepository<PassField> _repPassField;

        public PassTemplateServiceTests()
            : base()
        {
            _testPassTemplateDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestPassTemplate");
            _passTemplateFileName = TestHelper.PassContainerConfig.PassTemplateFileName;
        }

        [SetUp]
        public virtual void InitEachTest()
        {
            _pcUnitOfWork = TestHelper.PassContainerUnitOfWork;
            //Repositories
            _repPassTemplate = _pcUnitOfWork.GetRepository<PassTemplate>();
            _repPassTemplateApple = _pcUnitOfWork.GetRepository<PassTemplateApple>();
            _repPassField = _pcUnitOfWork.GetRepository<PassField>();
            if (Directory.Exists(_testPassTemplateDir))
                Directory.Delete(_testPassTemplateDir, true);
            Directory.CreateDirectory(_testPassTemplateDir);
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
            GeneralPassTemplate generalPassTemplate;
            int passTemplateId;

            using (var passTemplateService = GetPassTemplateService())
            {
                generalPassTemplate = TestHelper.PreparePassTemplateSource(_testPassTemplateDir, _passTemplateFileName);
                passTemplateId = passTemplateService.CreatePassTemlate(_testPassTemplateDir);
                Assert.Greater(passTemplateId, 0);
            }
            //Check pass template DB record
            PassTemplate passTemplate = _repPassTemplate.Find(passTemplateId);
            Assert.IsNotNull(passTemplate);
            Assert.AreEqual(1, passTemplate.Version);
            Assert.AreEqual(generalPassTemplate.TemplateName, passTemplate.Name);
            Assert.AreEqual(TemplateStatus.Active, passTemplate.Status);

            //Check native pass template
            IQueryable<PassTemplateApple> passTemplatesApple =
                _repPassTemplateApple.Query().Filter(x => x.PassTemplateId == passTemplateId).Get();
            Assert.Greater(passTemplatesApple.Count(), 0);
            Assert.AreEqual(1, passTemplatesApple.Count());

            //Check pass dynamic fields
            IQueryable<PassField> passFields =
                _repPassField.Query().Filter(x => x.PassTemplateId == passTemplateId).Get();
            Assert.Greater(passFields.Count(), 0);

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
                Assert.IsTrue(Directory.GetFiles(dir).Any(x => (Path.GetExtension(x) == ".json") || (Path.GetExtension(x) == ".xml")));
            }
        }

        [Test]
        public void GetPassFieldsTest()
        {
            using (var passTemplateService = GetPassTemplateService())
            {
                TestHelper.PreparePassTemplateSource(_testPassTemplateDir, _passTemplateFileName);
                int passTemplateId = passTemplateService.CreatePassTemlate(_testPassTemplateDir);
                Assert.Greater(passTemplateId, 0);
                IList<PassField> passFields = passTemplateService.GetPassFields(passTemplateId);
                Assert.IsNotNull(passFields);
                Assert.IsTrue(passFields.Select(x => x.Name).Contains("TestDynamicField"));
            }
        }

        [Test]
        public void UpdatePassTemplateTest()
        {
            GeneralPassTemplate generalPassTemplate;
            int passTemplateId;
            using (var passTemplateService = GetPassTemplateService())
            {
                generalPassTemplate = TestHelper.PreparePassTemplateSource(_testPassTemplateDir, _passTemplateFileName);
                passTemplateId = passTemplateService.CreatePassTemlate(_testPassTemplateDir);
                Assert.Greater(passTemplateId, 0);
               TestHelper.PreparePassTemplateSource(_testPassTemplateDir, _passTemplateFileName);
                Thread.Sleep(3000);
                passTemplateService.UpdatePassTemlate(passTemplateId, _testPassTemplateDir);
            }

            //Check pass template DB record
            PassTemplate passTemplate = _repPassTemplate.Find(passTemplateId);
            Assert.IsNotNull(passTemplate);
            Assert.AreEqual(2, passTemplate.Version);
            Assert.AreEqual(generalPassTemplate.TemplateName, passTemplate.Name);

            //Check native pass template
            IQueryable<PassTemplateApple> passTemplatesApple =
                _repPassTemplateApple.Query().Filter(x => x.PassTemplateId == passTemplateId).Get();
            Assert.Greater(passTemplatesApple.Count(), 0);
            Assert.AreEqual(1, passTemplatesApple.Count());

            //Check pass dynamic fields
            IQueryable<PassField> passFields =
                _repPassField.Query().Filter(x => x.PassTemplateId == passTemplateId).Get();
            Assert.Greater(passFields.Count(), 0);

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
                Assert.IsTrue(Directory.GetFiles(dir).Any(x => (Path.GetExtension(x) == ".json") || (Path.GetExtension(x) == ".xml")));
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
            Assert.AreEqual(TemplateStatus.Active, passTemplate.Status);

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

        private IFileStorageService GetFileStorageService()
        {
            return FileStorageFactory.Create(new FileStorageConfig());
        }
    }
}
