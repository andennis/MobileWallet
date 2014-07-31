using System;
using Common.Repository;
using NUnit.Framework;
using Pass.Manager.Repository.Core;
using Pass.Manager.Repository.Core.Entities;

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

    }
}
