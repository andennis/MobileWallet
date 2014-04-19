using System;
using CertificateStorage.Repository.Core;
using CertificateStorage.Repository.Core.Entities;
using NUnit.Framework;

namespace CertificateStorage.Repository.EF.Tests
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
