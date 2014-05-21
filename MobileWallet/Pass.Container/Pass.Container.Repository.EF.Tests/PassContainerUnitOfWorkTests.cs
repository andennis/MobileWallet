using System;
using NUnit.Framework;
using Pass.Container.Repository.Core;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.Repository.EF.Tests
{
    [TestFixture]
    public class PassContainerUnitOfWorkTests
    {
        private class UnknownEntity
        {
        }

        [Test]
        public void PassRepositoryTest()
        {
            using (var unitOfWork = TestHelper.GetPassContainerUnitOfWork())
            {
                Assert.IsNotNull(unitOfWork.PassRepository);
                Assert.IsInstanceOf<IPassRepository>(unitOfWork.PassRepository);
                Assert.IsInstanceOf<IPassRepository>(unitOfWork.GetRepository<Core.Entities.Pass>());
            }
        }

        [Test]
        public void GetRepositoryTest()
        {
            using (var unitOfWork = TestHelper.GetPassContainerUnitOfWork())
            {
                Assert.IsNotNull(unitOfWork.GetRepository<Core.Entities.Pass>());
                Assert.IsNotNull(unitOfWork.GetRepository<PassField>());
                Assert.IsNotNull(unitOfWork.GetRepository<PassFieldValue>());
                Assert.IsNotNull(unitOfWork.GetRepository<PassTemplate>());
                Assert.IsNotNull(unitOfWork.GetRepository<PassTemplateApple>());
                Assert.IsNotNull(unitOfWork.GetRepository<ClientDeviceApple>());
                Assert.IsNotNull(unitOfWork.GetRepository<Registration>());

                Assert.Throws<Exception>(() => unitOfWork.GetRepository<UnknownEntity>());
            }
        }

    }
}
