using System.Data.Entity.Infrastructure;

namespace Pass.Container.Repository.EF
{
    public class MigrationsContextFactory : IDbContextFactory<PassContainerDbContext>
    {
        public PassContainerDbContext Create()
        {
            return new PassContainerDbContext("MobileWalletConnection");
        }
    }
}
