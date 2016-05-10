using PoliceSystem.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PoliceSystem.DAL
{
    public class PoliceDbInitializer : DropCreateDatabaseAlways<PoliceDbContext>
    {

        protected override void Seed(PoliceDbContext db)
        {
            System.Diagnostics.Debug.WriteLine("Seeding database...");

            UserGroup adminGroup = new UserGroup();
            adminGroup.Name = "admin";
        
            UserGroup defaultGroup = new UserGroup();
            defaultGroup.Name = "default";

            db.UserGroups.Add(adminGroup);
            db.UserGroups.Add(defaultGroup);
            db.SaveChanges();

            User sam = new User();
            sam.Username = "samjansen16@gmail.com";
            sam.Password = "test";
            sam.UserGroup = adminGroup;

            User eric = new User();
            eric.Username = "ericderegter@gmail.com";
            eric.Password = "test";
            eric.UserGroup = adminGroup;

            db.Users.Add(sam);
            db.Users.Add(eric);
            db.SaveChanges();

            base.Seed(db);
        }
    }
}