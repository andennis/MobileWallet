using System.Data.Entity;
using FileStorage.Core.Entities;

namespace FileStorage.Repository.EF
{
    public sealed class FileStorageDbContext : DbContext
    {
        public const string DbScheme = "fs";

        public FileStorageDbContext(string nameOrConnectionString)
            :base(nameOrConnectionString)
        {
            //Database.SetInitializer<FileStorageDbContext>(null);
            //this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<FolderItem> FolderItems { get; set; }
        public DbSet<StorageItem> StorageItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FolderItem>().ToTable("FolderItem", DbScheme);
            modelBuilder.Entity<FolderItem>().HasOptional(x => x.Parent).WithMany(x => x.ChildFolders).Map(x => x.MapKey("ParentId"));
            modelBuilder.Entity<FolderItem>().Property(x => x.Name).IsRequired().HasMaxLength(400);

            modelBuilder.Entity<StorageItem>().ToTable("StorageItem", DbScheme);
            modelBuilder.Entity<StorageItem>().HasRequired(x => x.Parent).WithMany(x => x.ChildStorageItems).Map(x => x.MapKey("ParentId"));
            modelBuilder.Entity<StorageItem>().Property(x => x.Name).IsRequired().HasMaxLength(400);
            modelBuilder.Entity<StorageItem>().Property(x => x.OriginalName).IsOptional().HasMaxLength(400);
            modelBuilder.Entity<StorageItem>().Property(x => x.Size).IsOptional();

            base.OnModelCreating(modelBuilder);
        }
    }
}
