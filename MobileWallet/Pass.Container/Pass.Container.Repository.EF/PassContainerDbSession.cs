using Common.Repository;
using Common.Repository.EF;

namespace Pass.Container.Repository.EF
{
    public class PassContainerDbSession : DbSession
    {
        public PassContainerDbSession(IDbConfig dbConfig) 
            : base(new PassContainerDbContext(dbConfig.ConnectionString))
        {
        }
    }
}
