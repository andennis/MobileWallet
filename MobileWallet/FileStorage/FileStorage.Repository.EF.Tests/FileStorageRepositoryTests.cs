using FileStorage.Core;
using NUnit.Framework;

namespace FileStorage.Repository.EF.Tests
{
    [TestFixture]
    public class FileStorageRepositoryTests : RepositoryTestsBase
    {
        [Test]
        public void GetFreeFolderItemTest()
        {
            IFileStorageRepository fsRep = new FileStorageRepository(_dbSession);
            Assert.DoesNotThrow(() => fsRep.GetFreeFolderItem(3, 3)); 
        }

        [Test]
        public void GetFolderItemPathTest()
        {
            IFileStorageRepository fsRep = new FileStorageRepository(_dbSession);
            Assert.DoesNotThrow(() => fsRep.GetFolderItemPath(0)); 
        }

        [Test]
        public void GetStorageItemPathTest()
        {
            IFileStorageRepository fsRep = new FileStorageRepository(_dbSession);
            Assert.DoesNotThrow(() => fsRep.GetStorageItemPath(0)); 
        }
    }
}
