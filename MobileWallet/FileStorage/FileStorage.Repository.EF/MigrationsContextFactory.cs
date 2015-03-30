using System.Data.Entity.Infrastructure;

namespace FileStorage.Repository.EF
{
    public class MigrationsContextFactory : IDbContextFactory<FileStorageDbContext>
    {
        public FileStorageDbContext Create()
        {
            return new FileStorageDbContext("Data Source=localhost;Initial Catalog=MobileWallet;User ID=sa;Password=sa1");
        }
    }
}
