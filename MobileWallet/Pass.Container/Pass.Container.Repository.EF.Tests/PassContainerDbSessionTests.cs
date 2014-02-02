using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Repository;
using NUnit.Framework;

namespace Pass.Container.Repository.EF.Tests
{
    [TestFixture]
    public class PassContainerDbSessionTests
    {
        [Test]
        public void DbContextTest()
        {
            using (IDbSession dbSession = new PassContainerDbSession(TestHelper.DbConfig))
            {
                Assert.NotNull(dbSession.DbContext);
                Assert.IsInstanceOf<PassContainerDbContext>(dbSession.DbContext);
            }
        }
    }
}
