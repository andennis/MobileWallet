using Common.Repository;
using Pass.Manager.Core.Repositories;

namespace Pass.Manager.Core
{
    public interface IPassManagerUnitOfWork : IUnitOfWork
    {
        IPassSiteRepository PassSiteRepository { get; }
    }
}
