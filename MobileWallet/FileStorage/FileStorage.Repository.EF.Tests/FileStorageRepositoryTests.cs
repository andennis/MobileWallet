using FileStorage.Core;
using NUnit.Framework;

namespace FileStorage.Repository.EF.Tests
{
    [TestFixture]
    public class FileStorageRepositoryTests : RepositoryTestsBase
    {
        private IFileStorageRepository _fsRep;

        public override void InitEachTest()
        {
            base.InitEachTest();
            _fsRep = new FileStorageRepository(_dbSession);
        }

        [Test]
        public void GetFreeFolderItemTest()
        {
            Assert.DoesNotThrow(() => _fsRep.GetFreeFolderItem(3, 3)); 
        }

        [Test]
        public void GetFolderItemPathTest()
        {
            Assert.DoesNotThrow(() => _fsRep.GetFolderItemPath(0)); 
        }

        [Test]
        public void GetStorageItemPathTest()
        {
            Assert.DoesNotThrow(() => _fsRep.GetStorageItemPath(0)); 
        }
    }
}
