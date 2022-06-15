using CRMEntities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMEntities.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasData(
            new UserRole
            {
                Id = 1,
                Name = "SuperAdmin",
                NormalizedName = "SUPERADMIN"
            },
            new UserRole
            {
                Id = 2,
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            },
            new UserRole
            {
                Id = 3,
                Name = "Manager",
                NormalizedName = "MANAGER"
            },
            new UserRole
            {
                Id = 4,
                Name = "Employee",
                NormalizedName = "EMPLOYEE"
            },
            new UserRole
            {
                Id = 5,
                Name = "User",
                NormalizedName = "USER"
            },
            new UserRole
            {
                Id = 6,
                Name = "Guest",
                NormalizedName = "GUEST"
            }
            );
       }

    }
}