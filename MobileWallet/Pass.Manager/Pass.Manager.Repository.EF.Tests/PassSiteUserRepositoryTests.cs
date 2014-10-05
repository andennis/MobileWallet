using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common.Repository;
using NUnit.Framework;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Repository.EF.Tests
{
    [TestFixture]
    public class PassSiteUserRepositoryTests : RepositoryTestBase<IPassManagerUnitOfWork>
    {
        protected override IPassManagerUnitOfWork CreateUnitOfWork()
        {
            return TestHelper.GetPassManagerUnitOfWork();
        }

        [Test]
        public void GetUnassignedUsersTest()
        {
            PassSite passSite = TestHelper.CreatePassSite();
            User user = TestHelper.GetNewUser();
            CreateEntity(user);
            var psu = new PassSiteUser() {PassSiteId = passSite.PassSiteId, UserId = user.UserId};

            using (IPassManagerUnitOfWork unitOfWork = CreateUnitOfWork())
            {
                IEnumerable<User> users1 = unitOfWork.PassSiteUserRepository.GetUnassignedUsers(passSite.PassSiteId);
                Assert.Greater(users1.Count(), 0);

                CreateEntity(psu);
                IEnumerable<User> users2 = unitOfWork.PassSiteUserRepository.GetUnassignedUsers(passSite.PassSiteId);
                Assert.AreEqual(users1.Count()-1, users2.Count());
            }
            DeleteEntity(psu);
            DeleteEntity(user);
            DeleteEntity(passSite);
        }
    }
}
