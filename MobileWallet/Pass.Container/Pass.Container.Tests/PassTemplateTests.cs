using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Common.Extensions;
using NUnit.Framework;
using Pass.Container.Core.Entities.Templates.PassTemplate;

namespace Pass.Container.BL.Tests
{
    [TestFixture]
    public class PassTemplateTests
    {
        [Test]
        public void PassTemplateXmlSerializationTest()
        {
            PassTemplate generalTemplate = TestHelper.GetPassTemplateObject();
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Template.xml");
            if (File.Exists(path))
                File.Delete(path);
            generalTemplate.SaveToXml(path);
            Assert.IsTrue(File.Exists(path));

            PassTemplate template;
            template = path.LoadFromXml<PassTemplate>();
            Assert.AreEqual(generalTemplate.TemplateName, template.TemplateName);
        }

        [Test]
        public void PassTemplateJsonSerializationTest()
        {
            PassTemplate generalTemplate = TestHelper.GetPassTemplateObject();
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Template.json");
            if (File.Exists(path))
                File.Delete(path);
            string json = generalTemplate.ObjectToJson();
            File.WriteAllText(path, json);
            Assert.IsTrue(File.Exists(path));

            string jsonResult = File.ReadAllText(path);
            PassTemplate template = jsonResult.JsonToObject<PassTemplate>();
            Assert.AreEqual(generalTemplate.TemplateName, template.TemplateName);
        }
    }
}
