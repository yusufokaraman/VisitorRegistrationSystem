using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VisitorRegistrationSystem.Domain.Entity;

namespace VisitorRegistrationSystem.Repository.Mappings
{
    public class UserLoginMap : IEntityTypeConfiguration<UserLogin>
    {


        public void Configure(EntityTypeBuilder<UserLogin> b)
        {
            // Composite primary key consisting of the LoginProvider and the key to use
            // with that provider
            b.HasKey(l => new { l.LoginProvider, l.ProviderKey });

            // Limit the size of the composite key columns due to common DB restrictions
            b.Property(l => l.LoginProvider).HasMaxLength(128);
            b.Property(l => l.ProviderKey).HasMaxLength(128);

            // Maps to the AspNetUserLogins table
            b.ToTable("AspNetUserLogins");
        }
    }
}
