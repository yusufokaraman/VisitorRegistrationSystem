using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VisitorRegistrationSystem.Domain.Entitiy;

namespace VisitorRegistrationSystem.Repository.Mappings
{
    public class UserRoleMap : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> b)
        {
            // Primary key
            b.HasKey(r => new { r.UserId, r.RoleId });

            // Maps to the AspNetUserRoles table
            b.ToTable("AspNetUserRoles");

            b.HasData(new UserRole
            {
                UserId = 1,
                RoleId = 1

            },
            new UserRole()
            {
                UserId = 2,
                RoleId = 2


            });
        }
    }
}
