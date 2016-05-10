using PoliceSystem.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PoliceSystem.DAL
{
    public class UserDaoImpl : UserDao
    {
        private UserGroupDao userGroupDao = new UserGroupDaoImpl();

        public void Create(User user)
        {
            using (PoliceDbContext db = new PoliceDbContext())
            {
                if (FindByUsername(user.Username) == null)
                {
                    UserGroup userGroup = userGroupDao.FindByName("default");
                    db.UserGroups.Attach(userGroup);
                    user.UserGroup = userGroup;
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                else
                {
                    //laat de gebruiker weten dat de username bezet is
                }
            }
        }

        public User FindById(int id)
        {
            using (PoliceDbContext db = new PoliceDbContext())
            {
                try
                {
                    return db.Users.Include(u => u.UserGroup).Single(u => u.Id == id);
                }
                catch (ArgumentNullException ex)
                {
                    //Zero or more than one users found
                }
                catch (InvalidOperationException ex)
                {
                    //Zero or more than one users found
                }
                return null;
            }
        }

        public User FindByUsername(string username)
        {
            using (PoliceDbContext db = new PoliceDbContext())
            {
                try
                {
                    return db.Users.Include(u => u.UserGroup).Single(u => u.Username == username);
                }
                catch (ArgumentNullException ex)
                {
                    //Zero or more than one users found
                }
                catch (InvalidOperationException ex)
                {
                    //Zero or more than one users found
                }
                return null;
            }
        }

        public void Remove(User user)
        {
            if (UserExists(user))
            {
                using (PoliceDbContext db = new PoliceDbContext())
                {
                    db.Users.Remove(user);
                }
            }
            else
            {
                //User doesnt exists.
            }
        }

        public bool UserExists(User user)
        {
            using (PoliceDbContext db = new PoliceDbContext())
            {
                return db.Users.Any(u => u.Username == user.Username && u.Password == user.Password);
            }
        }
    }
}