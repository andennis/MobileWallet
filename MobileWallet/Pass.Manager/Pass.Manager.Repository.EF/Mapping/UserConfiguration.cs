using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Repository.EF.Mapping
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration(string dbScheme)
        {
            ToTable("User", dbScheme);
            Property(x => x.Version).IsConcurrencyToken();
            Property(x => x.UserName).IsRequired().HasMaxLength(512)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_User_Name") { IsUnique = true }));
            Property(x => x.FirstName).HasMaxLength(512);
            Property(x => x.LastName).HasMaxLength(512);
            Property(x => x.Password).HasMaxLength(512);
        }
    }
}
