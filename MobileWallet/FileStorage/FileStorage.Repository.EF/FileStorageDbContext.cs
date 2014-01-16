using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileStorage.Core.Entities;

namespace FileStorage.Repository.EF
{
    public class FileStorageDbContext : DbContext
    {
        private const string DbScheme = "fs";

        public FileStorageDbContext(string nameOrConnectionString)
            :base(nameOrConnectionString)
        {
            Database.SetInitializer<FileStorageDbContext>(null);
        }

        public DbSet<ItemInfo> Items { get; set; }
        public DbSet<FileInfo> Files { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemInfo>().ToTable("Item", DbScheme);
            modelBuilder.Entity<ItemInfo>().Property(x => x.Name).IsRequired().HasMaxLength(400);
            modelBuilder.Entity<ItemInfo>().HasOptional(x => x.ParentItem).WithMany(x => x.ChildItems).Map(x => x.MapKey("ParentId"));

            modelBuilder.Entity<FileInfo>().ToTable("File", DbScheme);
            modelBuilder.Entity<FileInfo>().Property(x => x.OriginalName).IsRequired().HasMaxLength(400);

            base.OnModelCreating(modelBuilder);
        }
    }
}
