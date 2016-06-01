using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PoliceSystem.Models.Domain;

namespace PoliceSystem.DAL
{
    public class UserGroupDaoImpl : UserGroupDao
    {

        public void Create(UserGroup userGroup)
        {
            using (PoliceDbContext db = new PoliceDbContext())
            {
                if (FindByName(userGroup.Name) == null)
                {
                    db.UserGroups.Add(userGroup);
                    db.SaveChanges();
                }
                else
                {
                    // Laat de gebruiker weten dat de username bezet is.
                }

            }
        }

        public UserGroup FindById(int id)
        {
            using (PoliceDbContext db = new PoliceDbContext())
            {
                return db.UserGroups.Single(ug => ug.Id == id);
            }
        }

        public UserGroup FindByName(string name)
        {
            using (PoliceDbContext db = new PoliceDbContext())
            {
                return db.UserGroups.Single(ug => ug.Name == name);
            }
        }

        public void Remove(UserGroup userGroup)
        {
            using (PoliceDbContext db = new PoliceDbContext())
            {
                db.UserGroups.Remove(userGroup);
            }
        }

        public void AddToUserGroup(UserGroup userGroup, User user)
        {
            //using (PoliceDbContext db = new PoliceDbContext())
            //{
            //    userGroup.Users.Add(user);

            //    db.UserGroups.Attach(userGroup);
            //    var userGroupEntry = db.Entry(userGroup);
            //    userGroupEntry.Property(ug => ug.Users).IsModified = true;

            //    user.UserGroup = userGroup;

            //    db.Users.Attach(user);
            //    var userEntry = db.Entry(user);
            //    userEntry.Property(u => u.UserGroup).IsModified = true;

            //    db.SaveChanges();
            //}
        }
    }
}