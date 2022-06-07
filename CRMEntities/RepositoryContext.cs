using CRMEntities.Configuration;
using CRMEntities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRMEntities
{
    public class RepositoryContext : IdentityDbContext<User, UserRole, long, IdentityUserClaim<long>, ApplicationUserRole, IdentityUserLogin<long>, RoleClaim, IdentityUserToken<long>>
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options)
                                : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            //modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            //modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

    }
}
