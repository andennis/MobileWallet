using System.Data.Entity.Infrastructure;

namespace Pass.Manager.Repository.EF
{
    public class MigrationsContextFactory : IDbContextFactory<PassManagerDbContext>
    {
        public PassManagerDbContext Create()
        {
            return new PassManagerDbContext("Data Source=localhost;Initial Catalog=MobileWallet;User ID=sa;Password=sa1");
        }
    }
}
