using System.Data.Entity.Infrastructure;

namespace Pass.CertificateStorage.Repository.EF
{
    public class MigrationsContextFactory : IDbContextFactory<CertificateStorageDbContext>
    {
        public CertificateStorageDbContext Create()
        {
            return new CertificateStorageDbContext("Data Source=localhost;Initial Catalog=MobileWallet;User ID=sa;Password=sa1");
        }
    }
}
