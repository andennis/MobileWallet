using Common.Repository;
using Common.Repository.EF;

namespace FileStorage.Repository.EF
{
    public class FileStorageDbSession : DbSession
    {
        public FileStorageDbSession(IDbConfig dbConfig) 
            : base(new FileStorageDbContext(dbConfig.ConnectionString))
        {
        }
    }
}
