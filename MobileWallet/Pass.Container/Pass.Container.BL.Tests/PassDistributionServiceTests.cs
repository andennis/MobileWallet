using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using FileStorage.BL;
using NUnit.Framework;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Factory;

namespace Pass.Container.BL.Tests
{
    [TestFixture]
    public class PassDistributionServiceTests
    {
        private readonly FileStorageConfig _fsConfig;

        public PassDistributionServiceTests()
        {
            _fsConfig = new FileStorageConfig();
        }

        [TestFixtureSetUp]
        public void InitAllTests()
        {
            TestHelper.ClearFileStorage(_fsConfig);
        }

        [Test]
        public void DisposeTest()
        {
            IPassDistributionService pds = GetPassDistributionService();
            Assert.IsInstanceOf<IDisposable>(pds);
            Assert.DoesNotThrow(pds.Dispose);
        }

        [Test]
        public void GetPassDistributionFieldsByTempleteTest()
        {
            using (IPassTemplateService templateService = GetPassTemplateService())
            using (IPassDistributionService distService = GetPassDistributionService())
            {
                string testPassTemplateDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestPassTemplate");
                TestHelper.PreparePassTemplateSource(testPassTemplateDir, TestHelper.PassContainerConfig.PassTemplateFileName);
                int passTemplateId = templateService.CreatePassTemlate(testPassTemplateDir);

                var passToken = new PassTokenInfo() {PassTemplateId = passTemplateId};
                IList<PassFieldInfo> fields = distService.GetPassDistributionFields(passToken);
                Assert.AreEqual(7, fields.Count);
            }
        }

        [Test]
        public void GetPassTokenTest()
        {
            using (IPassDistributionService distService = GetPassDistributionService())
            {
                string token = distService.GetPassToken(121);
                Assert.IsNotNullOrEmpty(token);

                PassTokenInfo pti = distService.DecryptPassToken(token);
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

                PassTokenInfo pti = distService.DecryptPassToken(token);
                Assert.NotNull(pti);
                Assert.AreEqual(125, pti.PassTemplateId);
                Assert.IsNull(pti.PassId);
            }
        }

        private IPassTemplateService GetPassTemplateService()
        {
            return PassContainerFactory.CreateTemplateService(new PassContainerConfig(), _fsConfig);
        }

        private IPassDistributionService GetPassDistributionService()
        {
            return PassContainerFactory.CreateDistributionService(new PassContainerConfig());
        }

    }
}
