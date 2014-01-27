using Common.Repository;
using NUnit.Framework;

namespace FileStorage.Repository.EF.Tests
{
    public class RepositoryTestsBase
    {
        protected IDbSession _dbSession;

        [SetUp]
        public void InitAllTests()
        {
            _dbSession = new FileStorageDbSession(TestHelper.DbConfig);
        }

        [TearDown]
        public void FinalizeAllTests()
        {
            _dbSession.Dispose();
        }

    }
}
