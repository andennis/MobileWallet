using Common.Repository;
using Pass.Manager.Core.Repositories;

namespace Pass.Manager.Core
{
    public interface IPassManagerUnitOfWork : IUnitOfWork
    {
        IPassSiteRepository PassSiteRepository { get; }
        IPassSiteUserRepository PassSiteUserRepository { get; }
        IPassSiteCertificateRepository PassSiteCertificateRepository { get; }
    }
}
