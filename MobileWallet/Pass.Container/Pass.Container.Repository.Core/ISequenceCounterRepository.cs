using Common.Repository;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.Repository.Core
{
    public interface ISequenceCounterRepository : IRepository<SequenceCounter>
    {
        int GetNextSerialNumber(string counterName);
    }
}