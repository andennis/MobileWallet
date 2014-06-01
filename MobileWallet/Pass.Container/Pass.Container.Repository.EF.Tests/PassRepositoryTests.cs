using System.Collections.Generic;
using NUnit.Framework;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.Repository.EF.Tests
{
    [TestFixture]
    public class PassRepositoryTests
    {
        [Test]
        public void GetChangedPassesAppleTest()
        {
            using (var unitOfWork = TestHelper.GetPassContainerUnitOfWork())
            {
                IList<ChangedPass> passes = unitOfWork.PassRepository.GetChangedPassesApple("123", "321", null);
                Assert.IsNotNull(passes);
                CollectionAssert.IsEmpty(passes);
            }
        }

    }
}
