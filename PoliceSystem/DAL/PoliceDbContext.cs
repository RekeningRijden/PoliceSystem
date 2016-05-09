﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using PoliceSystem.Models;
using PoliceSystem.Models.Domain;
using MySql.Data.Entity;

namespace PoliceSystem.DAL
{
    public class PoliceDbContext : DbContext
    {
        public PoliceDbContext() : base("name=PoliceDbContextConnectionString")
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<User>()
                    .HasRequired<UserGroup>(u => u.UserGroup) // User entity requires UserGroup 
                    .WithMany(ug => ug.Users); // Standard entity includes many Students entities
        }
    }
}