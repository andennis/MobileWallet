using System.Data.Entity;
using Common.Repository.EF;
using FileStorage.Repository.Core.Entities;
using FileStorage.Repository.EF.Mapping;

namespace FileStorage.Repository.EF
{
    public sealed class FileStorageDbContext : DbContextBase
    {
        public FileStorageDbContext(string nameOrConnectionString)
            :base(nameOrConnectionString)
        {
            //Database.SetInitializer<FileStorageDbContext>(null);
            //this.Configuration.LazyLoadingEnabled = false;
        }

        public override string DbScheme { get { return "fs"; } }

        //public DbSet<FolderItem> FolderItems { get; set; }
        public DbSet<StorageItem> StorageItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add<FolderItem>(new FolderItemConfiguration(DbScheme));
            modelBuilder.Configurations.Add<StorageItem>(new StorageItemConfiguration(DbScheme));

            base.OnModelCreating(modelBuilder);
        }
    }
}
