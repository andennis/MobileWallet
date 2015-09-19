using System;
using Common.BL;
using Moq;
using NUnit.Framework;
using Pass.Container.Repository.Core;

namespace Pass.Container.BL.Tests
{
    [TestFixture]
    public class SequenceGeneratorTests
    {
        [Test]
        public void GetNextValueTest()
        {
            var mockRep = new Mock<ISequenceCounterRepository>();
                
            string counterName1 = Guid.NewGuid().ToString();
            mockRep.SetupSequence(x => x.GetNextSerialNumber(counterName1)).Returns(1).Returns(2);

            string counterName2 = Guid.NewGuid().ToString();
            mockRep.SetupSequence(x => x.GetNextSerialNumber(counterName2)).Returns(1).Returns(2);

            var mockUnitOfWork = new Mock<IPassContainerUnitOfWork>();
            mockUnitOfWork.Setup(x => x.SequenceCounterRepository).Returns(mockRep.Object);
            ISequenceGenerator<int> snc = new SequenceGenerator(mockUnitOfWork.Object);
                
            int serNum1 = snc.GetNextValue(counterName1);
            Assert.AreEqual(1, serNum1);
            serNum1 = snc.GetNextValue(counterName1);
            Assert.AreEqual(2, serNum1);

            int serNum2 = snc.GetNextValue(counterName2);
            Assert.AreEqual(1, serNum2);
            serNum2 = snc.GetNextValue(counterName2);
            Assert.AreEqual(2, serNum2);
        }
    }
}
