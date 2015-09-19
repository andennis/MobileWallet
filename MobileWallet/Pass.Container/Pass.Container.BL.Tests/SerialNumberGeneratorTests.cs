using System;
using Common.BL;
using Moq;
using NUnit.Framework;
using Pass.Container.Core;

namespace Pass.Container.BL.Tests
{
    [TestFixture]
    public class SerialNumberGeneratorTests
    {
        [Test]
        public void GetNextSerialNumberTest()
        {
            var mockSeqGen = new Mock<ISequenceGenerator<int>>();
            ISerialNumberGenerator sng = new SerialNumberGenerator(mockSeqGen.Object);

            string counterName1 = Guid.NewGuid().ToString();
            mockSeqGen.SetupSequence(x => x.GetNextValue(counterName1)).Returns(1).Returns(2);
            string serNum1 = sng.GetNextSerialNumber(counterName1);
            Assert.AreEqual("1", serNum1);
            serNum1 = sng.GetNextSerialNumber(counterName1);
            Assert.AreEqual("2", serNum1);

            string counterName2 = Guid.NewGuid().ToString();
            mockSeqGen.SetupSequence(x => x.GetNextValue(counterName2)).Returns(1).Returns(2);
            string serNum2 = sng.GetNextSerialNumber(counterName2);
            Assert.AreEqual("1", serNum2);
            serNum2 = sng.GetNextSerialNumber(counterName2);
            Assert.AreEqual("2", serNum2);
        }
    }
}
