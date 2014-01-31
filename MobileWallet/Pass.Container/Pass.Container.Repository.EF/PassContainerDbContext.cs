using System.Data.Entity;
using PassEntities = Pass.Container.Core.Entities;

namespace Pass.Container.Repository.EF
{
    public class PassContainerDbContext : DbContext
    {
        public const string DbScheme = "pscn";

        public PassContainerDbContext(string nameOrConnectionString)
            :base(nameOrConnectionString)
        {
            //Database.SetInitializer<FileStorageDbContext>(null);
            //this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<PassEntities.Pass> Passes { get; set; }
        public DbSet<PassEntities.PassTemplate> PassTemplates { get; set; }
        public DbSet<PassEntities.PassField> PassFields { get; set; }
        public DbSet<PassEntities.PassFieldValue> PassFieldValues { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PassEntities.Pass>().ToTable("Pass", DbScheme);
            modelBuilder.Entity<PassEntities.Pass>().HasKey(x => x.PassId);
            modelBuilder.Entity<PassEntities.Pass>().Property(x => x.AuthToken).IsRequired().HasMaxLength(400);
            modelBuilder.Entity<PassEntities.Pass>().Property(x => x.ExpirationDate).IsOptional();
            modelBuilder.Entity<PassEntities.Pass>().Property(x => x.SerialNumber).IsRequired().HasMaxLength(400);
            modelBuilder.Entity<PassEntities.Pass>().Property(x => x.Status).IsRequired();
            modelBuilder.Entity<PassEntities.Pass>().Property(x => x.UpdatedDate).IsRequired();
            modelBuilder.Entity<PassEntities.Pass>().HasMany(x => x.FieldValues).WithRequired(x => x.Pass);

            modelBuilder.Entity<PassEntities.PassTemplate>().ToTable("PassTemplate", DbScheme);
            modelBuilder.Entity<PassEntities.PassTemplate>().HasKey(x => x.PassTemplateId);
            modelBuilder.Entity<PassEntities.PassTemplate>().Property(x => x.Name).IsRequired().HasMaxLength(400);
            modelBuilder.Entity<PassEntities.PassTemplate>().HasMany(x => x.PassFields).WithRequired(x => x.Template);
            modelBuilder.Entity<PassEntities.PassTemplate>().HasMany(x => x.Passes).WithRequired(x => x.Template);
            modelBuilder.Entity<PassEntities.PassTemplate>().HasMany(x => x.NativeTemplates).WithRequired(x => x.Template);

            modelBuilder.Entity<PassEntities.PassField>().ToTable("PassField", DbScheme);
            modelBuilder.Entity<PassEntities.PassField>().HasKey(x => x.PassFieldId);
            modelBuilder.Entity<PassEntities.PassField>().Property(x => x.Name).IsRequired().HasMaxLength(400);

            modelBuilder.Entity<PassEntities.PassFieldValue>().ToTable("PassFieldValue", DbScheme);
            modelBuilder.Entity<PassEntities.PassFieldValue>().HasKey(x => x.PassFieldValueId);
            modelBuilder.Entity<PassEntities.PassFieldValue>().Property(x => x.Value).IsOptional().HasMaxLength(400);
            //modelBuilder.Entity<PassEntities.PassFieldValue>().HasRequired(x => x.PassField).WithOptional(x => x.FieldValue);

            base.OnModelCreating(modelBuilder);
        }
    }
}
