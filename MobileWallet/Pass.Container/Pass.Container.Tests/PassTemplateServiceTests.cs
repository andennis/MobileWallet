using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using Common.Extensions;
using Common.Repository;
using FileStorage.BL;
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
    public class PassTemplateServiceTests : BlTestsBase
    {
        //Repositories
        private readonly IRepository<PassTemplate> _repPassTemplate;
        private readonly IRepository<PassTemplateNative> _repTemplateNative;
        private readonly IRepository<PassTemplateApple> _repPassTemplateApple;
        private readonly IRepository<PassField> _repPassField;

        public PassTemplateServiceTests():base()
        {
            _pcUnitOfWork = new PassContainerUnitOfWork(TestHelper.PassContainerConfig);
            //Repositories
            _repPassTemplate = _pcUnitOfWork.GetRepository<PassTemplate>();
            _repTemplateNative = _pcUnitOfWork.GetRepository<PassTemplateNative>();
            _repPassTemplateApple = _pcUnitOfWork.GetRepository<PassTemplateApple>();
            _repPassField = _pcUnitOfWork.GetRepository<PassField>();
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
            GeneralPassTemplate generalPassTemplate = PreparePassTemplateSource();
            int passTemplateId = _passTemplateService.CreatePassTemlate(_testPassTemplateDir);
            Assert.Greater(passTemplateId, 0);

            //Check pass template DB record
            PassTemplate passTemplate = _repPassTemplate.Find(passTemplateId);
            Assert.IsNotNull(passTemplate);
            Assert.AreEqual(1, passTemplate.Version);
            Assert.AreEqual(generalPassTemplate.TemplateName, passTemplate.Name);
            Assert.AreEqual(TemplateStatus.Active, passTemplate.Status);
            
            //Check native pass template
            IQueryable<PassTemplateApple> passTemplatesApple = _repPassTemplateApple.Query().Filter(x => x.PassTemplateId == passTemplateId).Get();
            Assert.Greater(passTemplatesApple.Count(), 0);
            Assert.AreEqual(1, passTemplatesApple.Count());

            //Check pass dynamic fields
            IQueryable<PassField> passFields = _repPassField.Query().Filter(x => x.PassTemplateId == passTemplateId).Get();
            Assert.Greater(passFields.Count(), 0);

            //Check file storage
            int packageId = passTemplate.PackageId;
            string storageItemPath = _fsService.GetStorageItemPath(packageId);
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
            PreparePassTemplateSource();
            int passTemplateId = _passTemplateService.CreatePassTemlate(_testPassTemplateDir);
            Assert.Greater(passTemplateId, 0);
            IList<PassField> passFields = _passTemplateService.GetPassFields(passTemplateId);
            Assert.IsNotNull(passFields);
            Assert.IsTrue(passFields.Select(x => x.Name).Contains("TestDynamicField"));
        }

        [Test]
        public void UpdatePassTemplateTest()
        {
            GeneralPassTemplate generalPassTemplate = PreparePassTemplateSource();
            int passTemplateId = _passTemplateService.CreatePassTemlate(_testPassTemplateDir);
            Assert.Greater(passTemplateId, 0);
            PreparePassTemplateSource();
            Thread.Sleep(3000);
            _passTemplateService.UpdatePassTemlate(passTemplateId, _testPassTemplateDir);

            //Check pass template DB record
            PassTemplate passTemplate = _repPassTemplate.Find(passTemplateId);
            Assert.IsNotNull(passTemplate);
            Assert.AreEqual(2, passTemplate.Version);
            Assert.AreEqual(generalPassTemplate.TemplateName, passTemplate.Name);

            //Check native pass template
            IQueryable<PassTemplateApple> passTemplatesApple = _repPassTemplateApple.Query().Filter(x => x.PassTemplateId == passTemplateId).Get();
            Assert.Greater(passTemplatesApple.Count(), 0);
            Assert.AreEqual(1, passTemplatesApple.Count());

            //Check pass dynamic fields
            IQueryable<PassField> passFields = _repPassField.Query().Filter(x => x.PassTemplateId == passTemplateId).Get();
            Assert.Greater(passFields.Count(), 0);

            //Check file storage
            int packageId = passTemplate.PackageId;
            string storageItemPath = _fsService.GetStorageItemPath(packageId);
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
            PreparePassTemplateSource();
            int passTemplateId = _passTemplateService.CreatePassTemlate(_testPassTemplateDir);
            Assert.Greater(passTemplateId, 0);

            PassTemplate passTemplate = _repPassTemplate.Find(passTemplateId);
            Assert.IsNotNull(passTemplate);
            Assert.AreEqual(TemplateStatus.Active, passTemplate.Status);

            //Check file storage
            int packageId = passTemplate.PackageId;
            string storageItemPath = _fsService.GetStorageItemPath(packageId);
            Assert.IsNotNull(storageItemPath);
            string[] directories = Directory.GetDirectories(storageItemPath);
            Assert.AreEqual(2, directories.Count());
            foreach (string dir in directories)
            {
                Assert.IsTrue(Directory.GetFiles(dir).Any(x => (Path.GetExtension(x) == ".json") || (Path.GetExtension(x) == ".xml")));
            }
            
            _passTemplateService.DeletePassTemplate(passTemplateId);
            
            //PassTemplate passTemplateAfterDelete = _repPassTemplate.Find(passTemplateId);
            //Assert.IsNotNull(passTemplateAfterDelete);
            //Assert.AreEqual(TemplateStatus.InActive, passTemplateAfterDelete.Status);

            //Check file storage
            directories = Directory.GetDirectories(storageItemPath);
            Assert.AreEqual(2, directories.Count());
            foreach (string dir in directories)
            {
                Assert.IsTrue(Directory.GetFiles(dir).Any(x => (Path.GetExtension(x) == ".json") || (Path.GetExtension(x) == ".xml")));
            }
            ////Check file storage item as Deleted
            //packageId = passTemplate.PackageId;
            //storageItemPath = _fsService.GetStorageItemPath(packageId);
            //Assert.IsNull(storageItemPath);
        }

        private GeneralPassTemplate PreparePassTemplateSource()
        {
            //Prepare pass template source
            GeneralPassTemplate generalTemplate = TestHelper.GetPassTemplateObject();
            generalTemplate.FieldDetails.AuxiliaryFields.Add(new Field
                {
                    Key = "TestDynamicField",
                    Value = "TestDynamicFieldValue",
                    Type = Field.DataType.Text,
                    IsDynamic = true
                });
            string path = Path.Combine(_testPassTemplateDir, TestHelper.PassContainerConfig.PassTemplateFileName);
            if (File.Exists(path))
                File.Delete(path);
            generalTemplate.SaveToXml(path);
            Assert.IsTrue(File.Exists(path));

            return generalTemplate;
        }

        private IPassTemplateService GetPassTemplateService()
        {
            return PassContainerFactory.CreateTemplateService(new PassContainerConfig(), new FileStorageConfig());
        }
    }
}
