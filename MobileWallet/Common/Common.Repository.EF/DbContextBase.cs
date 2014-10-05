using System.Data.Entity;

namespace Common.Repository.EF
{
    public abstract class DbContextBase : DbContext
    {
        protected DbContextBase(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }
        
        public abstract string DbScheme { get; }
    }
}
