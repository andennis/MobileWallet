using System;
using NUnit.Framework;
using Pass.Container.Core;
using Common.Repository;

namespace Pass.Container.BL.Tests
{
    [TestFixture]
    public class PassContainerConfigTests
    {
        [Test]
        public void PassContainerConfigPropertiesTest()
        {
            var pcConfig = new PassContainerConfig();

            Assert.IsNotNullOrEmpty(pcConfig.ConnectionString);
            Assert.IsNotNullOrEmpty(pcConfig.PassWorkingFolder);

            //TODO check all other config parametes
            throw new NotImplementedException();
        }

        [Test]
        public void PassContainerConfigInterfacesTest()
        {
            var pcConfig = new PassContainerConfig();
            Assert.IsInstanceOf<IPassContainerConfig>(pcConfig);
            Assert.IsInstanceOf<IPassTemplateConfig>(pcConfig);
            Assert.IsInstanceOf<IDbConfig>(pcConfig);
            Assert.IsInstanceOf<IApplePassGeneratorConfig>(pcConfig);
        }
    }
}
