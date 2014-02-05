using System.Data.Entity.Infrastructure;

namespace Pass.Container.Repository.EF
{
    public class MigrationsContextFactory : IDbContextFactory<PassContainerDbContext>
    {
        public PassContainerDbContext Create()
        {
            return new PassContainerDbContext("Data Source=localhost;Initial Catalog=MobileWallet;User ID=sa;Password=sa1");
        }
    }
}
