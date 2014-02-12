using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Common.Extensions;
using FileStorage.BL;
using NUnit.Framework;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Templates.GeneralPassTemplate;
using Pass.Container.Factory;

namespace Pass.Container.BL.Tests
{
    [TestFixture]
    public class PassTemplateServiceTests : BlTestsBase
    {
        private readonly IPassContainerConfig _ptConfig;

        public PassTemplateServiceTests()
        {
            _ptConfig = new PassContainerConfig();
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
            PreparePassTemplateSource();
            int passTemplateId = _passTemplateService.CreatePassTemlate(_testPassTemplateDir);
            Assert.Greater(passTemplateId, 0);
        }

        [Test]
        public void GetPassFieldsTest()
        {
            PreparePassTemplateSource();
            int passTemplateId = _passTemplateService.CreatePassTemlate(_testPassTemplateDir);
            Assert.Greater(0, passTemplateId);
            IList<PassField> passFields = _passTemplateService.GetPassFields(passTemplateId);
            Assert.IsNotNull(passFields);
            Assert.IsTrue(passFields.Select(x => x.Name).Contains("TestDynamicField"));
        }

        [Test]
        public void UpdatePassTemplateTest()
        {
            PreparePassTemplateSource();
            int passTemplateId = _passTemplateService.CreatePassTemlate(_testPassTemplateDir);
            Assert.Greater(0, passTemplateId);
            _passTemplateService.UpdatePassTemlate(passTemplateId, _testPassTemplateDir);
        }

        [Test]
        public void TempTest1()
        {
            using (IPassTemplateService ptService = GetPassTemplateService())
            {
                
            }
        }

        private void PreparePassTemplateSource()
        {
            //Prepare pass template source
            GeneralPassTemplate generalTemplate = TestHelper.GetPassTemplateObject();
            generalTemplate.FieldDetails.AuxiliaryFields.Add(new Field
                {
                    Key = "TestDynamicField",
                    Value = "TestDynamicFieldValue",
                    Type = Field.DataType.Text
                });
            string path = Path.Combine(_testPassTemplateDir, TestHelper.PassContainerConfig.PassTemplateFileName);
            if (File.Exists(path))
                File.Delete(path);
            generalTemplate.SaveToXml(path);
            Assert.IsTrue(File.Exists(path));
        }
        private IPassTemplateService GetPassTemplateService()
        {
            return PassContainerFactory.Create(_ptConfig, new FileStorageConfig());
        }
    }
}
