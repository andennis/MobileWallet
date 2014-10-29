using System;
using Common.Repository;
using NUnit.Framework;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Repository.EF.Tests
{
    [TestFixture]
    public class PassManagerRepositoryTests : RepositoryTestBase<IPassManagerUnitOfWork>
    {
        protected override IPassManagerUnitOfWork CreateUnitOfWork()
        {
            return TestHelper.GetPassManagerUnitOfWork();
        }

        [Test]
        public void PassSiteTest()
        {
            var passSite1 = new PassSite()
                            {
                                Name = Guid.NewGuid().ToString(),
                                Description = "D1"
                            };
            CreateEntity(passSite1);
            Assert.Greater(passSite1.PassSiteId, 0);

            var passSite2 = ReadEntity<PassSite>(passSite1.PassSiteId);
            Assert.NotNull(passSite2);
            Assert.AreEqual(passSite1.Name, passSite2.Name);
            Assert.AreEqual(passSite1.Description, passSite2.Description);

            passSite2.Name = passSite1.Name + "_New";
            passSite2.Description = "D2";
            UpdateEntity(passSite2);

            var passSite3 = ReadEntity<PassSite>(passSite1.PassSiteId);
            Assert.NotNull(passSite3);
            Assert.AreEqual(passSite2.Name, passSite3.Name);
            Assert.AreEqual(passSite2.Description, passSite3.Description);

            DeleteEntity(passSite3);

            var passSite4 = ReadEntity<PassSite>(passSite1.PassSiteId);
            Assert.Null(passSite4);
        }

        [Test]
        public void PassProjectTest()
        {
            PassSite passSite = TestHelper.CreatePassSite();
            var passPrj1 = new PassProject()
                          {
                              Name = Guid.NewGuid().ToString(),
                              Description = Guid.NewGuid().ToString(),
                              PassSiteId = passSite.PassSiteId
                          };
            CreateEntity(passPrj1);
            Assert.Greater(passPrj1.PassProjectId, 0);

            var passPrj2 = ReadEntity<PassProject>(passPrj1.PassProjectId);
            Assert.NotNull(passPrj2);
            Assert.AreEqual(passPrj1.Name, passPrj2.Name);
            Assert.AreEqual(passPrj1.Description, passPrj2.Description);

            passPrj2.Name = passPrj1.Name + "_New";
            passPrj2.Description = "D2";
            UpdateEntity(passPrj2);

            var passPrj3 = ReadEntity<PassProject>(passPrj1.PassProjectId);
            Assert.NotNull(passPrj3);
            Assert.AreEqual(passPrj2.Name, passPrj3.Name);
            Assert.AreEqual(passPrj2.Description, passPrj3.Description);

            DeleteEntity(passPrj3);

            var passSite4 = ReadEntity<PassProject>(passPrj1.PassSiteId);
            Assert.Null(passSite4);

        }

        [Test]
        public void UserTest()
        {
            var user1 = new User()
            {
                UserName = Guid.NewGuid().ToString(),
                FirstName = "FN"+Guid.NewGuid().ToString(),
                LastName = "LN" + Guid.NewGuid().ToString()
            };
            CreateEntity(user1);
            Assert.Greater(user1.UserId, 0);

            var user2 = ReadEntity<User>(user1.UserId);
            Assert.NotNull(user2);
            Assert.AreEqual(user1.UserName, user2.UserName);
            Assert.AreEqual(user1.FirstName, user2.FirstName);
            Assert.AreEqual(user1.LastName, user2.LastName);

            user2.FirstName = user1.FirstName + "_New";
            user2.LastName = user1.LastName + "_New";
            UpdateEntity(user2);

            var user3 = ReadEntity<User>(user1.UserId);
            Assert.NotNull(user3);
            Assert.AreEqual(user2.FirstName, user3.FirstName);
            Assert.AreEqual(user2.LastName, user3.LastName);

            DeleteEntity(user3);

            var user4 = ReadEntity<User>(user1.UserId);
            Assert.Null(user4);
        }

        [Test]
        public void PassCertificateTest()
        {
            var cert1 = new PassCertificateApple()
                       {
                           Name = GetGuid(),
                           ExpDate = DateTime.Now.Date,
                           PassTypeId = "PType1",
                           TeamId = "Team1",
                           CertificateStorageId = 111
                       };

            CreateEntity(cert1);
            Assert.Greater(cert1.PassCertificateId, 0);

            var cert2 = ReadEntity<PassCertificateApple>(cert1.PassCertificateId);
            Assert.NotNull(cert2);
            Assert.AreEqual(cert1.Name, cert2.Name);
            Assert.AreEqual(cert1.ExpDate, cert2.ExpDate);
            Assert.AreEqual(cert1.PassTypeId, cert2.PassTypeId);
            Assert.AreEqual(cert1.TeamId, cert2.TeamId);
            Assert.AreEqual(cert1.CertificateStorageId, cert2.CertificateStorageId);

            cert2.Name = cert1.Name + "_New";
            cert2.ExpDate = DateTime.Now.Date.AddDays(1);
            cert2.PassTypeId = cert1.PassTypeId + "_New";
            cert2.TeamId = cert1.TeamId + "_New";
            cert2.CertificateStorageId = 222;
            UpdateEntity(cert2);

            var cert3 = ReadEntity<PassCertificateApple>(cert1.PassCertificateId);
            Assert.NotNull(cert3);
            Assert.AreEqual(cert2.Name, cert3.Name);
            Assert.AreEqual(cert2.ExpDate, cert3.ExpDate);
            Assert.AreEqual(cert2.PassTypeId, cert3.PassTypeId);
            Assert.AreEqual(cert2.TeamId, cert3.TeamId);
            Assert.AreEqual(cert2.CertificateStorageId, cert3.CertificateStorageId);

            DeleteEntity(cert3);

            var cert4 = ReadEntity<PassCertificateApple>(cert1.PassCertificateId);
            Assert.Null(cert4);
        }

        [Test]
        public void PassSiteUserTest()
        {
            PassSite passSite = CreateEntity(TestHelper.GetNewPassSite());
            User user = CreateEntity(TestHelper.GetNewUser());

            var siteUser1 = new PassSiteUser()
                           {
                               PassSiteId = passSite.PassSiteId,
                               UserId = user.UserId,
                               Status = EntityStatus.InActive
                           };
            CreateEntity(siteUser1);
            var siteUser2 = ReadEntity<PassSiteUser>(siteUser1.PassSiteUserId);
            Assert.NotNull(siteUser2);
            Assert.AreEqual(siteUser1.Status, siteUser2.Status);

            siteUser2.Status = EntityStatus.Active;
            UpdateEntity(siteUser2);

            var siteUser3 = ReadEntity<PassSiteUser>(siteUser1.PassSiteUserId);
            Assert.NotNull(siteUser3);
            Assert.AreEqual(siteUser2.Status, siteUser3.Status);

            DeleteEntity(siteUser3);
            var siteUser4 = ReadEntity<PassSiteUser>(siteUser1.PassSiteUserId);
            Assert.Null(siteUser4);

            DeleteEntity(passSite);
            DeleteEntity(user);
        }

        [Test]
        public void PassSiteCertificateTest()
        {
            PassSite passSite = CreateEntity(TestHelper.GetNewPassSite());
            PassCertificate cert = CreateEntity(TestHelper.GetNewAppleCertificate());

            var siteCert1 = new PassSiteCertificate()
            {
                PassSiteId = passSite.PassSiteId,
                PassCertificateId = cert.PassCertificateId,
            };
            CreateEntity(siteCert1);
            var siteCert2 = ReadEntity<PassSiteCertificate>(siteCert1.PassSiteCertificateId);
            Assert.NotNull(siteCert2);

            DeleteEntity(siteCert2);
            var siteUser3 = ReadEntity<PassSiteCertificate>(siteCert1.PassSiteCertificateId);
            Assert.Null(siteUser3);

            DeleteEntity(passSite);
            DeleteEntity(cert);
        }

    }
}
