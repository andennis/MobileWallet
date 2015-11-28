using System.Data.Entity;
using Common.Repository.EF;
using Pass.Container.Repository.Core.Entities;
using Pass.Container.Repository.EF.Mapping;

namespace Pass.Container.Repository.EF
{
    public class PassContainerDbContext : DbContextBase
    {
        public PassContainerDbContext(string nameOrConnectionString)
            :base(nameOrConnectionString)
        {
            //Database.SetInitializer<PassContainerDbContext>(null);
            //this.Configuration.LazyLoadingEnabled = false;
        }

        public override string DbScheme { get { return "pscn"; } }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add<PassTemplate>(new PassTemplateConfiguration(DbScheme));
            modelBuilder.Configurations.Add<PassField>(new PassFieldConfiguraion(DbScheme));
            modelBuilder.Configurations.Add<Core.Entities.Pass>(new PassConfiguration(DbScheme));
            modelBuilder.Configurations.Add<PassNative>(new PassNativeConfiguration(DbScheme));
            modelBuilder.Configurations.Add<PassApple>(new PassAppleConfiguration(DbScheme));
            modelBuilder.Configurations.Add<PassFieldValue>(new PassFieldValueConfiguration(DbScheme));
            modelBuilder.Configurations.Add<PassTemplateNative>(new PassTemplateNativeConfiguration(DbScheme));
            modelBuilder.Configurations.Add<PassTemplateApple>(new PassTemplateAppleConfiguration(DbScheme));
            modelBuilder.Configurations.Add<ClientDevice>(new ClientDeviceConfiguration(DbScheme));
            modelBuilder.Configurations.Add<ClientDeviceApple>(new ClientDeviceAppleConfiguration(DbScheme));
            modelBuilder.Configurations.Add<Registration>(new RegistrationConfiguration(DbScheme));
            modelBuilder.Configurations.Add<SequenceCounter>(new SequenceCounterConfiguration(DbScheme));
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
