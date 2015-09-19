using Common.BL;
using Pass.Container.Repository.Core;

namespace Pass.Container.BL
{
    public class SequenceGenerator : ISequenceGenerator<int>
    {
        private readonly IPassContainerUnitOfWork _pcUnitOfWork;

        public SequenceGenerator(IPassContainerUnitOfWork pcUnitOfWork)
        {
            _pcUnitOfWork = pcUnitOfWork;
        }

        public int GetNextValue(string counterName)
        {
            return _pcUnitOfWork.SequenceCounterRepository.GetNextSerialNumber(counterName);
        }
    }
}
