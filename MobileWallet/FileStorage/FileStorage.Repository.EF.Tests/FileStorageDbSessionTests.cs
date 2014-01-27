using Common.Repository;
using NUnit.Framework;

namespace FileStorage.Repository.EF.Tests
{
    [TestFixture]
    public class FileStorageDbSessionTests
    {
        [Test]
        public void DbContextTest()
        {
            using (IDbSession dbSession = new FileStorageDbSession(TestHelper.DbConfig))
            {
                Assert.NotNull(dbSession.DbContext);
                Assert.IsInstanceOf<FileStorageDbContext>(dbSession.DbContext);
            }
        }
    }
}
