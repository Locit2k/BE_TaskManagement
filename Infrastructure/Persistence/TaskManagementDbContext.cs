using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class TaskManagementDbContext : DbContext
    {
        public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options)
            : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Notifications> Notifications { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(x => x.RecID);
                entity.Property(x => x.RecID).IsRequired();
                entity.Property(x => x.ModifiedBy).IsRequired(false);
                entity.Property(x => x.ModifiedOn).IsRequired(false);
            });
            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(x => x.RecID);
                entity.Property(x => x.RecID).IsRequired();
                entity.Property(x => x.Address).IsRequired(false);
                entity.Property(x => x.ModifiedBy).IsRequired(false);
                entity.Property(x => x.ModifiedOn).IsRequired(false);
            });
            modelBuilder.Entity<Tasks>(entity =>
            {
                entity.HasKey(x => x.RecID);
                entity.Property(x => x.RecID).IsRequired();
                entity.Property(x => x.Description).IsRequired(false);
                entity.Property(x => x.Priority).IsRequired(false);
                entity.Property(x => x.Owner).IsRequired(false);
                entity.Property(x => x.AssignBy).IsRequired(false);
                entity.Property(x => x.ModifiedBy).IsRequired(false);
                entity.Property(x => x.ModifiedOn).IsRequired(false);
            });
            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(x => x.RecID);
                entity.Property(x => x.RecID).IsRequired();
                entity.Property(x => x.ModifiedBy).IsRequired(false);
                entity.Property(x => x.ModifiedOn).IsRequired(false);
            });
            modelBuilder.Entity<Notifications>(entity =>
            {
                entity.HasKey(x => x.RecID);
                entity.Property(x => x.RecID).IsRequired();
                entity.Property(x => x.ModifiedBy).IsRequired(false);
                entity.Property(x => x.ModifiedOn).IsRequired(false);
            });
        }
    }
}
