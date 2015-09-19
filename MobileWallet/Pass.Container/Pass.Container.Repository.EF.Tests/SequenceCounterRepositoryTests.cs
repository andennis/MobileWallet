using System.Linq;
using NUnit.Framework;
using Pass.Container.Repository.Core;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.Repository.EF.Tests
{
    [TestFixture]
    public class SequenceCounterRepositoryTests
    {
        private const string TestCounter1 = "TestCounter1";
        private const string TestCounter2 = "TestCounter2"; 

        [TestFixtureSetUp]
        public void InitAllTests()
        {
            using (var unitOfWork = TestHelper.GetPassContainerUnitOfWork())
            {
                ISequenceCounterRepository sncRep = unitOfWork.SequenceCounterRepository;

                SequenceCounter snc = sncRep.Query().Filter(x => x.Name == TestCounter1).Get().FirstOrDefault();
                if (snc != null)
                {
                    sncRep.Delete(snc);
                    unitOfWork.Save();
                }

                snc = sncRep.Query().Filter(x => x.Name == TestCounter2).Get().FirstOrDefault();
                if (snc != null)
                {
                    sncRep.Delete(snc);
                    unitOfWork.Save();
                }
            }
        }

        [Test]
        public void GetNextSerialNumberTest()
        {
            using (var unitOfWork = TestHelper.GetPassContainerUnitOfWork())
            {
                int counterValue = unitOfWork.SequenceCounterRepository.GetNextSerialNumber(TestCounter1);
                Assert.AreEqual(1, counterValue);

                counterValue = unitOfWork.SequenceCounterRepository.GetNextSerialNumber(TestCounter1);
                Assert.AreEqual(2, counterValue);

                counterValue = unitOfWork.SequenceCounterRepository.GetNextSerialNumber(TestCounter1);
                Assert.AreEqual(3, counterValue);
            }

            using (var unitOfWork = TestHelper.GetPassContainerUnitOfWork())
            {
                int counterValue = unitOfWork.SequenceCounterRepository.GetNextSerialNumber(TestCounter2);
                Assert.AreEqual(1, counterValue);

                counterValue = unitOfWork.SequenceCounterRepository.GetNextSerialNumber(TestCounter2);
                Assert.AreEqual(2, counterValue);

                counterValue = unitOfWork.SequenceCounterRepository.GetNextSerialNumber(TestCounter2);
                Assert.AreEqual(3, counterValue);
            }
        }
    }
}
