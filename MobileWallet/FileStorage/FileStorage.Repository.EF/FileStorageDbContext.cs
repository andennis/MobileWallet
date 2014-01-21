using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
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

        public int? GetFreeFolder(int level, int maxItemsNumber)
        {
            IList<int> lst = this.Database.SqlQuery<int>(DbScheme + ".[GetFreeFolder] @Level, @MaxItemsNumber",
                new SqlParameter("Level", level), new SqlParameter("MaxItemsNumber", maxItemsNumber)).ToList();
            if (lst.Any())
                return lst[0];

            return null;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FolderItem>().ToTable("FolderItem", DbScheme);
            modelBuilder.Entity<FolderItem>().HasKey(x => x.ItemId);
            modelBuilder.Entity<FolderItem>().HasOptional(x => x.Parent).WithMany(x => x.ChildFolders).Map(x => x.MapKey("ParentId"));
            modelBuilder.Entity<FolderItem>().Property(x => x.ItemId).HasColumnName("FolderItemId");
            modelBuilder.Entity<FolderItem>().Property(x => x.Name).IsRequired().HasMaxLength(400);

            modelBuilder.Entity<FileItem>().ToTable("FileItem", DbScheme);
            modelBuilder.Entity<FileItem>().HasKey(x => x.ItemId);
            modelBuilder.Entity<FileItem>().HasRequired(x => x.Parent).WithMany(x => x.ChildFiles).Map(x => x.MapKey("ParentId"));
            modelBuilder.Entity<FileItem>().Property(x => x.ItemId).HasColumnName("FileItemId");
            modelBuilder.Entity<FileItem>().Property(x => x.Name).IsRequired().HasMaxLength(400);
            modelBuilder.Entity<FileItem>().Property(x => x.OriginalName).IsRequired().HasMaxLength(400);

            base.OnModelCreating(modelBuilder);
        }
    }
}
