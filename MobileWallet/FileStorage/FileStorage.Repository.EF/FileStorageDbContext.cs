using System.Data.Entity;
using FileStorage.Core.Entities;

namespace FileStorage.Repository.EF
{
    public class FileStorageDbContext : DbContext
    {
        private const string DbScheme = "fs";

        public FileStorageDbContext(string nameOrConnectionString)
            :base(nameOrConnectionString)
        {
            //Database.SetInitializer<FileStorageDbContext>(null);
        }

        public DbSet<FolderItem> Items { get; set; }
        public DbSet<FileItem> Files { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FolderItem>().ToTable("FolderItem", DbScheme);
            modelBuilder.Entity<FolderItem>().Property(x => x.Name).IsRequired().HasMaxLength(400);
            modelBuilder.Entity<FolderItem>().HasOptional(x => x.Parent).WithMany(x => x.ChildFolders).Map(x => x.MapKey("ParentId"));

            modelBuilder.Entity<FileItem>().ToTable("FileItem", DbScheme);
            modelBuilder.Entity<FileItem>().Property(x => x.Name).IsRequired().HasMaxLength(400);
            modelBuilder.Entity<FileItem>().Property(x => x.OriginalName).IsRequired().HasMaxLength(400);
            modelBuilder.Entity<FileItem>().HasRequired(x => x.Parent).WithMany(x => x.ChildFiles).Map(x => x.MapKey("ParentId"));

            base.OnModelCreating(modelBuilder);
        }
    }
}
