using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileStorage.BL;
using NUnit.Framework;
using Pass.Container.Core;
using Pass.Container.Factory;

namespace Pass.Container.BL.Tests
{
    [TestFixture]
    public class PassContainerServiceTests
    {
        [Test]
        public void DisposeTest()
        {
            IPassContainerService pcs = GetPassContainerService();
            Assert.IsInstanceOf<IDisposable>(pcs);
            Assert.DoesNotThrow(pcs.Dispose);
        }

        [Test]
        public void CreatePassTest()
        {
            using (var pts = GetPassTemplateService())
            using (var pcs = GetPassContainerService())
            {
                //pts.CreatePassTemlate()
                //pcs.CreatePass()
            }
        }

        [Test]
        public void UpdatePassFieldsTest()
        {
            
        }

        private IPassContainerService GetPassContainerService()
        {
            return PassContainerFactory.CreateContainerService(new PassContainerConfig());
        }

        private IPassTemplateService GetPassTemplateService()
        {
            return PassContainerFactory.CreateTemplateService(new PassContainerConfig(), new FileStorageConfig());
        }

    }
}
