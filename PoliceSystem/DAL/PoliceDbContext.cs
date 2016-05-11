using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using PoliceSystem.Models.Domain;
using MySql.Data.Entity;

namespace PoliceSystem.DAL
{
    public class PoliceDbContext : DbContext
    {
        public PoliceDbContext() : base("name=PoliceDbContextConnectionString")
        {
            Database.SetInitializer<PoliceDbContext>(new PoliceDbInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Theftinfo> Theftinfos { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<User>()
                    .HasRequired<UserGroup>(u => u.UserGroup) // User entity requires UserGroup 
                    .WithMany()
                    .WillCascadeOnDelete(false); // Standard entity includes many Students entities

            modelBuilder.Entity<Theftinfo>()
                  .HasOptional<Car>(t => t.Car) // User entity requires UserGroup 
                  .WithMany()
                  .WillCascadeOnDelete(false); // Standard entity includes many Students entities

            modelBuilder.Entity<Theftinfo>()
              .HasOptional(t => t.LastSeenLocation);

            modelBuilder.Entity<Theftinfo>()
              .HasOptional(t => t.CarFoundLocation);
        }
    }
}