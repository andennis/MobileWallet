using System;
using System.Collections.Generic;
using System.IO;
using Common.Extensions;
using NUnit.Framework;
using Pass.Container.BL.Helpers;
using Pass.Container.BL.PassTemplateGenerators;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Core.Entities.Templates.GeneralPassTemplate;

namespace Pass.Container.BL.Tests.PassTemplateGenerators
{
    [TestFixture]
    public class ApplePassTemplateGeneratorTests
    {
        private const string TemplateFolder = @"PassTemplateGenerators\Data\PassTemplate";

        [Test]
        public void ClientTypeTest()
        {
            var generator = new ApplePassTemplateGenerator();
            Assert.AreEqual(ClientType.Apple, generator.ClientType);
        }

        [Test]
        public void GenerateTest()
        {
            string templateFilePath = Path.Combine(TemplateFolder, "template.xml");
            var genPassTemplate = templateFilePath.LoadFromXml<GeneralPassTemplate>();

            string dstTemplateFolder = Path.Combine(TemplateFolder, "Apple");
            if (Directory.Exists(dstTemplateFolder))
            {
                Directory.Delete(dstTemplateFolder, true);
            }
            Directory.CreateDirectory(dstTemplateFolder);

            string imageFilesPath = Path.Combine(TemplateFolder, "Images");

            var generator = new ApplePassTemplateGenerator();
            Assert.DoesNotThrow(() => generator.Generate(genPassTemplate, Directory.EnumerateFiles(imageFilesPath), dstTemplateFolder));

            string dstImageFilesPath = Path.Combine(dstTemplateFolder, ApplePass.TemplateImageFolder);
            string[] dstImageFiles = Directory.GetFiles(dstImageFilesPath);
            Assert.AreEqual(1, dstImageFiles.Length);
            Assert.AreEqual("icon.png", Path.GetFileName(dstImageFiles[0]));

            string passContentFilePath = Path.Combine(dstTemplateFolder, ApplePass.PassTemplateFileName);
            Assert.True(File.Exists(passContentFilePath));

            string manifestFilePath = Path.Combine(dstTemplateFolder, ApplePass.ManifestTemplateFileName);
            Assert.True(File.Exists(manifestFilePath));

            string manifestContent = File.ReadAllText(manifestFilePath);
            var manifest = manifestContent.JsonToObject<Dictionary<string, string>>();
            Assert.AreEqual(1, manifest.Count);
            Assert.True(manifest.ContainsKey("icon.png"));
            Assert.IsNotNullOrEmpty(manifest["icon.png"]);

            Assert.Throws<ArgumentNullException>(() => generator.Generate(null, new string[0], "123"));
            Assert.Throws<ArgumentNullException>(() => generator.Generate(genPassTemplate, new string[0], null));
        }
    }
}
