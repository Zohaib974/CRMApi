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

            //-----Event and Contact many to many configuration
            modelBuilder.Entity<EventContact>()
                .HasKey(bc => new { bc.EventId, bc.ContactId });
            modelBuilder.Entity<EventContact>()
                .HasOne(bc => bc.Event)
                .WithMany(b => b.EventContacts)
                .HasForeignKey(bc => bc.EventId);
            modelBuilder.Entity<EventContact>()
                .HasOne(bc => bc.Contact)
                .WithMany(c => c.EventContacts)
                .HasForeignKey(bc => bc.ContactId);

            //-----Job and Contact many to many configuration
            modelBuilder.Entity<JobContact>()
                .HasKey(bc => new { bc.JobId, bc.ContactId });
            modelBuilder.Entity<JobContact>()
                .HasOne(bc => bc.Job)
                .WithMany(b => b.JobContacts)
                .HasForeignKey(bc => bc.JobId);
            modelBuilder.Entity<JobContact>()
                .HasOne(bc => bc.Contact)
                .WithMany(c => c.JobContacts)
                .HasForeignKey(bc => bc.ContactId);

            //-----Related Contacts many to many
            modelBuilder.Entity<RelatedContact>()
                .HasKey(bc => new { bc.RelContactId, bc.ContactId });

            modelBuilder.Entity<Contact>()
                .HasMany(u => u.RelatedContacts)
                .WithOne(f => f.Contact)
                .HasForeignKey(f => f.ContactId);

            //modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            //modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            //modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<EventContact> EventContacts { get; set; }
        public DbSet<JobContact> JobContacts { get; set; }
        public DbSet<RelatedContact> RelatedContacts { get; set; }
        public DbSet<UserColumn> UserColumns { get; set; }

    }
}
