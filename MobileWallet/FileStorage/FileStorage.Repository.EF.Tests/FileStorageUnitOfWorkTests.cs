using System;
using FileStorage.Repository.Core;
using FileStorage.Repository.Core.Entities;
using NUnit.Framework;

namespace FileStorage.Repository.EF.Tests
{
    [TestFixture]
    public class FileStorageUnitOfWorkTests : RepositoryTestsBase
    {
        private class UnknownEntity
        {
        }

        [Test]
        public void FileStorageRepositoryTest()
        {
            Assert.NotNull(_unitOfWork.FileStorageRepository);
            Assert.IsInstanceOf<IFileStorageRepository>(_unitOfWork.FileStorageRepository);
        }

        [Test]
        public void GetRepositoryTest()
        {
            Assert.IsNotNull(_unitOfWork.GetRepository<StorageItem>());
            Assert.IsNotNull(_unitOfWork.GetRepository<FolderItem>());
            Assert.Throws<Exception>(() => _unitOfWork.GetRepository<UnknownEntity>());
        }
    }
}
