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
                Assert.IsNotNull(unitOfWork.GetRepository<PassProjectField>());
                Assert.IsNotNull(unitOfWork.GetRepository<PassContentTemplate>());
                Assert.IsNotNull(unitOfWork.GetRepository<PassContentTemplateField>());
                Assert.IsNotNull(unitOfWork.GetRepository<PassImage>());

                Assert.IsNotNull(unitOfWork.PassSiteRepository);
                Assert.IsNotNull(unitOfWork.PassSiteUserRepository);
                Assert.IsNotNull(unitOfWork.PassSiteCertificateRepository);
                Assert.IsNotNull(unitOfWork.PassContentTemplateFieldRepository);

                Assert.Throws<Exception>(() => unitOfWork.GetRepository<UnknownEntity>());
            }
        }
    }
}
