using System.Data.Entity.Infrastructure;

namespace Pass.CertificateStorage.Repository.EF
{
    public class MigrationsContextFactory : IDbContextFactory<PassCertificateStorageDbContext>
    {
        public PassCertificateStorageDbContext Create()
        {
            return new PassCertificateStorageDbContext("Data Source=localhost;Initial Catalog=MobileWallet;User ID=sa;Password=sa1");
        }
    }
}
