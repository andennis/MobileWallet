using System;
using NUnit.Framework;
using Pass.Container.Core.Entities;

namespace Pass.Container.Repository.EF.Tests
{
    [TestFixture]
    public class PassContainerUnitOfWorkTests
    {
        private class MyEntity
        {
        }

        [Test]
        public void GetRepositoryTest()
        {
            using (var dbSession = new PassContainerDbSession(TestHelper.DbConfig))
            using (var unitOfWork = new PassContainerUnitOfWork(dbSession))
            {
                Assert.IsNotNull(unitOfWork.GetRepository<PassApple>());
                Assert.IsNotNull(unitOfWork.GetRepository<PassTemplate>());
                Assert.IsNotNull(unitOfWork.GetRepository<PassTemplateApple>());
                Assert.IsNotNull(unitOfWork.GetRepository<ClientDeviceApple>());
                Assert.IsNotNull(unitOfWork.GetRepository<Registration>());
                Assert.Throws<Exception>(() => unitOfWork.GetRepository<MyEntity>());
            }
        }
    }
}
