using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PoliceSystem.Models;

namespace PoliceSystem.DAL
{
    public class UserDaoImpl : UserDao
    {
        public void Create(User user)
        {
            using (PoliceDbContext db = new PoliceDbContext())
            {
                if (FindByUsername(user.Username) == null)
                {
                    //Set usergroup
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
                    return db.Users.Single(u => u.Id == id);
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
                    return db.Users.Single(u => u.Username == username);
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