using System.Data.Entity;

namespace Common.Repository.EF
{
    public class DbSession : IDbSession
    {
        private readonly DbContext _dbContext;

        public DbSession(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public object DbContext
        {
            get { return _dbContext; }
        }
    }
}
