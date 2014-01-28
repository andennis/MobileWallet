using FileStorage.Core;
using FileStorage.Core.Entities;
using NUnit.Framework;

namespace FileStorage.Repository.EF.Tests
{
    [TestFixture]
    public class FileStorageUnitOfWorkTests : RepositoryTestsBase
    {
        [Test]
        public void FileStorageRepositoryTest()
        {
            var unitOfWork = new FileStorageUnitOfWork(_dbSession);
            Assert.NotNull(unitOfWork.FileStorageRepository);
            Assert.IsInstanceOf<IFileStorageRepository>(unitOfWork.FileStorageRepository);
        }

        [Test]
        public void GetRepositoryForStorageItemTest()
        {
            var unitOfWork = new FileStorageUnitOfWork(_dbSession);
            Assert.IsNotNull(unitOfWork.GetRepository<StorageItem>());
        }
    }
}
