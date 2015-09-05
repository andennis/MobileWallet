using NUnit.Framework;
using Pass.Container.Core.Entities;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.BL.Tests
{
    [TestFixture]
    public class EntityConverterTests
    {
        [Test]
        public void RepositoryFieldValueToPassFieldInfo()
        {
            var pf = new PassField() {Name = "F1"};
            var pfv = new PassFieldValue() {PassFieldId = 1, Label = "L1", Value = "V1", PassField = pf};

            PassFieldInfo pfi = EntityConverter.RepositoryFieldValueToPassFieldInfo(pfv);
            Assert.NotNull(pfi);
            Assert.AreEqual(pfv.Label, pfi.Label);
            Assert.AreEqual(pfv.Value, pfi.Value);

            pfv.Label = null;
            pfv.Value = null;
            pfi = EntityConverter.RepositoryFieldValueToPassFieldInfo(pfv);
            Assert.NotNull(pfi);
            Assert.Null(pfi.Label);
            Assert.Null(pfi.Value);
        }

        /*
        [Test]
        public void ClientTypeToRepositoryClientDeviceTypeTest()
        {
            Assert.AreEqual(ClientType.Unknown, EntityConverter.ClientTypeToRepositoryClientDeviceType(ClientType.Unknown));
            Assert.AreEqual(ClientType.Apple, EntityConverter.ClientTypeToRepositoryClientDeviceType(ClientType.Apple));
            Assert.AreEqual(ClientType.Browser, EntityConverter.ClientTypeToRepositoryClientDeviceType(ClientType.Browser));
        }
        */
    }
}
