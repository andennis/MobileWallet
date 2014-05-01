using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Pass.Container.Core;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Factory;

namespace Pass.Container.BL.Tests
{
    [TestFixture]
    public class PassTemplateStorageServiceTests
    {
        private const string TemplateStorageData = @"Data\TemplateStorage";
        private IPassTemplateStorageService _tsService;

        [TestFixtureSetUp]
        public void InitAllTests()
        {
            _tsService = PassContainerFactory.GetPassTemplateStorageService();
        }

        [Test]
        public void BaseTemplateStorageTest()
        {
            int templateStorageId = _tsService.CreateTemplateStorage();
            Assert.Greater(templateStorageId, 0);

            Assert.DoesNotThrow(() => _tsService.PutBaseTemplateFiles(templateStorageId, Path.Combine(TemplateStorageData, "Base")));

            string dstFolder = Path.Combine(TemplateStorageData, "Result");
            if (Directory.Exists(dstFolder))
                Directory.Delete(dstFolder, true);

            Directory.CreateDirectory(dstFolder);
            Assert.DoesNotThrow(() => _tsService.GetBaseTemplateFiles(templateStorageId, dstFolder));
            IEnumerable<string> files = Directory.GetFiles(dstFolder).Select(Path.GetFileName);
            Assert.AreEqual(2, files.Count());
            CollectionAssert.Contains(files, "BaseFile1.txt");
            CollectionAssert.Contains(files, "BaseFile2.txt");
            Assert.True(Directory.Exists(Path.Combine(TemplateStorageData, "Base", "F1")));
            Assert.True(File.Exists(Path.Combine(TemplateStorageData, "Base", "F1", "BaseFile11.txt")));
        }

        [Test]
        public void NativeTemplateStorageTest()
        {
            int templateStorageId = _tsService.CreateTemplateStorage();
            Assert.Greater(templateStorageId, 0);

            Assert.DoesNotThrow(() => _tsService.PutNativeTemplateFiles(templateStorageId, ClientType.Apple, Path.Combine(TemplateStorageData, "Apple")));

            string dstFolder = Path.Combine(TemplateStorageData, "Result");
            if (Directory.Exists(dstFolder))
                Directory.Delete(dstFolder, true);

            Directory.CreateDirectory(dstFolder);
            Assert.DoesNotThrow(() => _tsService.GetNativeTemplateFiles(templateStorageId, ClientType.Apple, dstFolder));
            IEnumerable<string> files = Directory.GetFiles(dstFolder).Select(Path.GetFileName);
            Assert.AreEqual(2, files.Count());
            CollectionAssert.Contains(files, "AppleFile1.txt");
            CollectionAssert.Contains(files, "AppleFile2.txt");
            Assert.True(Directory.Exists(Path.Combine(TemplateStorageData, "Apple", "F1")));
            Assert.True(File.Exists(Path.Combine(TemplateStorageData, "Apple", "F1", "AppleFile11.txt")));
        }

    }
}
