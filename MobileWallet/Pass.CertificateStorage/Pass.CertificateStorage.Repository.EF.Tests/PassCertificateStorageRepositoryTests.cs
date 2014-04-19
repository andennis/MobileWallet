using Common.Repository;
using NUnit.Framework;
using Pass.CertificateStorage.Repository.Core;
using Pass.CertificateStorage.Repository.Core.Entities;

namespace Pass.CertificateStorage.Repository.EF.Tests
{
    [TestFixture]
    public class PassCertificateStorageRepositoryTests
    {
        [Test]
        public void CertificateCrudOperationsTest()
        {
            using (ICertificateStorageUnitOfWork unitOfWork = new CertificateStorageUnitOfWork(TestHelper.DbConfig))
            {
                IRepository<Certificate> certRep = unitOfWork.GetRepository<Certificate>();
                var cert1 = new Certificate()
                               {
                                   Name = "Cert1",
                                   Password = "123",
                                   FileId = 1,
                                   Status = EntityStatus.Active,
                               };
                certRep.Insert(cert1);
                unitOfWork.Save();
                Assert.Greater(cert1.CertificateId, 0);

                Certificate cert2 = certRep.Find(cert1.CertificateId);
                Assert.NotNull(cert2);
                Assert.AreEqual(cert1.CertificateId, cert2.CertificateId);
                Assert.AreEqual(cert1.Name, cert2.Name);
                Assert.AreEqual(cert1.Password, cert2.Password);
                Assert.AreEqual(cert1.FileId, cert2.FileId);
                Assert.AreEqual(cert1.Status, cert2.Status);

                cert1.Password = "321";
                cert1.Status = EntityStatus.InActive;
                certRep.Update(cert1);
                unitOfWork.Save();
                cert2 = certRep.Find(cert1.CertificateId);
                Assert.AreEqual(cert1.Password, cert2.Password);
                Assert.AreEqual(cert1.Status, cert2.Status);

                certRep.Delete(cert1);
                unitOfWork.Save();
                Assert.IsNull(certRep.Find(cert1.CertificateId));
            }
        }
    }
}
