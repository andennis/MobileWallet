using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.BL.Tests
{
    [TestFixture]
    public class EntityConverterTests
    {
        [Test]
        public void RepositoryFieldValueToPassFieldInfo()
        {
            var pf = new PassField() {DefaultLabel = "DL1", DefaultValue = "DV1"};
            var pfv = new PassFieldValue() {PassFieldId = 1, Label = "L1", Value = "V1", PassField = pf};

            PassFieldInfo pfi = EntityConverter.RepositoryFieldValueToPassFieldInfo(pfv, true);
            Assert.NotNull(pfi);
            Assert.AreEqual(pfv.Label, pfi.Label);
            Assert.AreEqual(pfv.Value, pfi.Value);

            pfi = EntityConverter.RepositoryFieldValueToPassFieldInfo(pfv, false);
            Assert.NotNull(pfi);
            Assert.AreEqual(pfv.Label, pfi.Label);
            Assert.AreEqual(pfv.Value, pfi.Value);

            pfv.Label = null;
            pfv.Value = null;
            pfi = EntityConverter.RepositoryFieldValueToPassFieldInfo(pfv, true);
            Assert.NotNull(pfi);
            Assert.AreEqual(pf.DefaultLabel, pfi.Label);
            Assert.AreEqual(pf.DefaultValue, pfi.Value);

            pfi = EntityConverter.RepositoryFieldValueToPassFieldInfo(pfv, false);
            Assert.NotNull(pfi);
            Assert.AreEqual(pfv.Label, pfi.Label);
            Assert.AreEqual(pfv.Value, pfi.Value);
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
