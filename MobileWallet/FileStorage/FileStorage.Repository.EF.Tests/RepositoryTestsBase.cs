using FileStorage.Repository.Core;
using NUnit.Framework;

namespace FileStorage.Repository.EF.Tests
{
    public class RepositoryTestsBase
    {
        protected IFileStorageUnitOfWork _unitOfWork;

        [SetUp]
        public virtual void InitEachTest()
        {
            _unitOfWork = new FileStorageUnitOfWork(TestHelper.DbConfig);
        }

        [TearDown]
        public virtual void FinalizeEachTest()
        {
            _unitOfWork.Dispose();
        }

    }
}