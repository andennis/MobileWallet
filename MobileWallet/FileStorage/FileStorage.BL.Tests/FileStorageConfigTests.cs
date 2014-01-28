using FileStorage.Core;
using NUnit.Framework;

namespace FileStorage.BL.Tests
{
    [TestFixture]
    public class FileStorageConfigTests
    {
        [Test]
        public void FileStorageConfigPropertiesTest()
        {
            var fsConfig = new FileStorageConfig() as IFileStorageConfig;
            Assert.NotNull(fsConfig);
            Assert.AreEqual(3, fsConfig.StorageDeep);
            Assert.AreEqual(2, fsConfig.MaxItemsNumber);
            Assert.NotNull(fsConfig.StoragePath);
            Assert.NotNull(fsConfig.ConnectionString);
        }
    }
}
