using System;
using NUnit.Framework;
using PassEntities = Pass.Container.Core.Entities;

namespace Pass.Container.Repository.EF.Tests
{
    [TestFixture]
    public class PassContainerUnitOfWorkTests
    {
        private class UnknownEntity
        {
        }

        [Test]
        public void GetRepositoryTest()
        {
            using (var unitOfWork = new PassContainerUnitOfWork(TestHelper.DbConfig))
            {
                Assert.IsNotNull(unitOfWork.GetRepository<PassEntities.Pass>());
                Assert.IsNotNull(unitOfWork.GetRepository<PassEntities.PassField>());
                Assert.IsNotNull(unitOfWork.GetRepository<PassEntities.PassFieldValue>());
                Assert.IsNotNull(unitOfWork.GetRepository<PassEntities.PassTemplate>());
                Assert.IsNotNull(unitOfWork.GetRepository<PassEntities.PassTemplateApple>());
                Assert.IsNotNull(unitOfWork.GetRepository<PassEntities.ClientDeviceApple>());
                Assert.IsNotNull(unitOfWork.GetRepository<PassEntities.Registration>());
                Assert.Throws<Exception>(() => unitOfWork.GetRepository<UnknownEntity>());
            }
        }
    }
}
