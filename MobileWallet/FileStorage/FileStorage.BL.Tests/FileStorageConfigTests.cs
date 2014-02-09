using Common.Repository;
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
            var fsConfig = new FileStorageConfig();
            Assert.AreEqual(3, fsConfig.StorageDeep);
            Assert.AreEqual(2, fsConfig.MaxItemsNumber);
            Assert.IsNotNullOrEmpty(fsConfig.StoragePath);
            Assert.IsNotNullOrEmpty(fsConfig.ConnectionString);
        }

        [Test]
        public void FileStorageConfigInterfacesTest()
        {
            var fsConfig = new FileStorageConfig();
            Assert.IsInstanceOf<IFileStorageConfig>(fsConfig);
            Assert.IsInstanceOf<IDbConfig>(fsConfig);
        }
    }
}
