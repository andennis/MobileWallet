using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Pass.Container.Repository.EF.Tests
{
    [TestFixture]
    public class PassRepositoryTests
    {
        [Test]
        public void GetPassSerialNumbersAppleTest()
        {
            using (var unitOfWork = TestHelper.GetPassContainerUnitOfWork())
            {
                IList<string> passes = unitOfWork.PassRepository.GetPassSerialNumbersApple("123", "321", DateTime.Now);
                Assert.IsNotNull(passes);
                CollectionAssert.IsEmpty(passes);
            }
        }
    }
}
