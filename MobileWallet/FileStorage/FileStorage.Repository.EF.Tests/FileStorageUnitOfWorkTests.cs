using FileStorage.Core;
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
    }
}
