using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Pass.Container.Core.Entities;

namespace Pass.Container.Repository.EF.Tests
{
    [TestFixture]
    public class PassContainerRepositoryTests
    {
        [Test]
        public void Test1()
        {
            var dbSession = new PassContainerDbSession(TestHelper.DbConfig);
            var uow = new PassContainerUnitOfWork(dbSession);
            var rep = uow.GetRepository<PassApple>();
            rep.Find(1);
        }
    }
}
