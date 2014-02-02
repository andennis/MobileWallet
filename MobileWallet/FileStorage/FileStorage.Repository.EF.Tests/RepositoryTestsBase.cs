using Common.Repository;
using NUnit.Framework;

namespace FileStorage.Repository.EF.Tests
{
    public class RepositoryTestsBase
    {
        protected IDbSession _dbSession;

        [SetUp]
        public virtual void InitEachTest()
        {
            _dbSession = new FileStorageDbSession(TestHelper.DbConfig);
        }

        [TearDown]
        public virtual void FinalizeEachTest()
        {
            _dbSession.Dispose();
        }

    }
}
