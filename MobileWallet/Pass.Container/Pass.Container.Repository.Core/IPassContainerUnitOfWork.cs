using Common.Repository;

namespace Pass.Container.Repository.Core
{
    public interface IPassContainerUnitOfWork : IUnitOfWork
    {
        IPassRepository PassRepository { get; }
        ISequenceCounterRepository SequenceCounterRepository { get; }
    }
}
