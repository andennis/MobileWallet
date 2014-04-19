using System;
using NUnit.Framework;
using Pass.CertificateStorage.Repository.Core;
using Pass.CertificateStorage.Repository.Core.Entities;

namespace Pass.CertificateStorage.Repository.EF.Tests
{
    [TestFixture]
    public class PassCertificateStorageUnitOfWorkTests
    {
        private class UnknownEntity
        {
        }

        [Test]
        public void GetRepositoryTest()
        {
            using (ICertificateStorageUnitOfWork unitOfWork = new CertificateStorageUnitOfWork(TestHelper.DbConfig))
            {
                Assert.IsNotNull(unitOfWork.GetRepository<Certificate>());
                Assert.Throws<Exception>(() => unitOfWork.GetRepository<UnknownEntity>());
            }
        }

    }
}
