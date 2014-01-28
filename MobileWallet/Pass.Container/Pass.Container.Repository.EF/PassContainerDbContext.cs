using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pass.Container.Core.Entities;

namespace Pass.Container.Repository.EF
{
    public class PassContainerDbContext : DbContext
    {
        public const string DbScheme = "pc";

        public PassContainerDbContext(string nameOrConnectionString)
            :base(nameOrConnectionString)
        {
            //Database.SetInitializer<FileStorageDbContext>(null);
            //this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<ClientPass> ClientPasses { get; set; }
        public DbSet<PassTemplateContainer> PassTemplates { get; set; }
        public DbSet<PassField> PassFields { get; set; }
        public DbSet<PassFieldValue> PassFieldValues { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientPass>().ToTable("ClientPass", DbScheme);
            modelBuilder.Entity<ClientPass>().HasKey(x => x.ClientPassId);
            modelBuilder.Entity<ClientPass>().Property(x => x.AuthToken).IsRequired().HasMaxLength(400);
            modelBuilder.Entity<ClientPass>().Property(x => x.ExpirationDate).IsOptional();
            modelBuilder.Entity<ClientPass>().Property(x => x.SerialNumber).IsRequired().HasMaxLength(400);
            modelBuilder.Entity<ClientPass>().Property(x => x.Status).IsRequired();
            modelBuilder.Entity<ClientPass>().Property(x => x.UpdatedDate).IsRequired();
            modelBuilder.Entity<ClientPass>().HasMany(x => x.PassFieldValues).WithRequired(x => x.ClientPass);

            modelBuilder.Entity<PassTemplateContainer>().ToTable("PassTemplate", DbScheme);
            modelBuilder.Entity<PassTemplateContainer>().HasKey(x => x.PassTemplateId);
            modelBuilder.Entity<PassTemplateContainer>().Property(x => x.Name).IsRequired().HasMaxLength(400);
            modelBuilder.Entity<PassTemplateContainer>().HasMany(x => x.PassFields).WithRequired(x => x.PassTemplate);
            modelBuilder.Entity<PassTemplateContainer>().HasMany(x => x.Passes).WithRequired(x => x.PassTemplate);
            modelBuilder.Entity<PassTemplateContainer>().HasMany(x => x.NativePassTemplates).WithRequired(x => x.PassTemplate);

            modelBuilder.Entity<PassField>().ToTable("PassField", DbScheme);
            modelBuilder.Entity<PassField>().HasKey(x => x.PassFieldId);
            modelBuilder.Entity<PassField>().Property(x => x.Name).IsRequired().HasMaxLength(400);

            modelBuilder.Entity<PassFieldValue>().ToTable("PassFieldValue", DbScheme);
            modelBuilder.Entity<PassFieldValue>().HasKey(x => x.PassFieldValueId);
            modelBuilder.Entity<PassFieldValue>().Property(x => x.Value).IsOptional().HasMaxLength(400);
            modelBuilder.Entity<PassFieldValue>().HasRequired(x => x.PassField).WithOptional(x => x.PassFieldValue);

            base.OnModelCreating(modelBuilder);
        }
    }
}
