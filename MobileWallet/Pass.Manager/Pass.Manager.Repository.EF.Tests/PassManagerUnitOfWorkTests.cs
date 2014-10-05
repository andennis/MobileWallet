using NUnit.Framework;
using System;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Repository.EF.Tests
{
    [TestFixture]
    public class PassManagerUnitOfWorkTests
    {
        private class UnknownEntity
        {
             
        }

        [Test]
        public void GetRepositoryTest()
        {
            using (var unitOfWork = TestHelper.GetPassManagerUnitOfWork())
            {
                Assert.IsNotNull(unitOfWork.GetRepository<PassSite>());
                Assert.IsNotNull(unitOfWork.GetRepository<PassProject>());
                Assert.IsNotNull(unitOfWork.GetRepository<User>());
                Assert.IsNotNull(unitOfWork.GetRepository<PassSiteUser>());
                Assert.IsNotNull(unitOfWork.GetRepository<PassCertificate>());
                Assert.IsNotNull(unitOfWork.GetRepository<PassCertificateApple>());
                Assert.IsNotNull(unitOfWork.GetRepository<PassSiteCertificate>());

                throw new NotImplementedException("Check custom repositories");

                Assert.Throws<Exception>(() => unitOfWork.GetRepository<UnknownEntity>());
            }
        }
    }
}
