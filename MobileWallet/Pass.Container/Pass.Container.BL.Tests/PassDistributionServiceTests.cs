using NUnit.Framework;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Factory;

namespace Pass.Container.BL.Tests
{
    [TestFixture]
    public class PassDistributionServiceTests
    {
        [Test]
        public void GetPassTokenTest()
        {
            using (IPassDistributionService distService = GetPassDistributionService())
            {
                string token = distService.GetPassToken(121);
                Assert.IsNotNullOrEmpty(token);

                PassTokenInfo pti = distService.GetPassTokenInfo(token);
                Assert.NotNull(pti);
                Assert.AreEqual(121, pti.PassId);
                Assert.IsNull(pti.PassTemplateId);
            }
        }

        [Test]
        public void GetPassTemplateTokenTest()
        {
            using (IPassDistributionService distService = GetPassDistributionService())
            {
                string token = distService.GetPassTemplateToken(125);
                Assert.IsNotNullOrEmpty(token);

                PassTokenInfo pti = distService.GetPassTokenInfo(token);
                Assert.NotNull(pti);
                Assert.AreEqual(125, pti.PassTemplateId);
                Assert.IsNull(pti.PassId);
            }
        }

        private IPassDistributionService GetPassDistributionService()
        {
            return PassContainerFactory.CreateDistributionService(new PassContainerConfig());
        }

    }
}
