using System;
using System.Data.Entity;

namespace Common.Repository.EF
{
    public class DbSession : IDbSession
    {
        protected readonly DbContext _dbContext;

        public DbSession(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");

            _dbContext = dbContext;
        }
        public object DbContext
        {
            get { return _dbContext; }
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
