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
                    throw new InvalidOperationException("Username already exists");
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

        public List<User> getAllUsers()
        {
            using (PoliceDbContext db =new PoliceDbContext())
            {
                return db.Users.Include(u => u.UserGroup).ToList();
            }
        }

        public void Remove(int id)
        {
            using (PoliceDbContext db = new PoliceDbContext())
            {
                User user = FindById(id);
                if (UserExists(user))
                {
                    if (user.UserGroup.Name.Equals("admin"))
                    {
                        throw new InvalidOperationException("Cannot delete admin account");
                    }

                    db.Users.Attach(user);
                    db.Users.Remove(user);
                    db.SaveChanges();
                }

                else
                {
                    //User doesnt exists.
                    throw new InvalidOperationException("User with username: " + user.Username + " does not exist");
                }
            }
        }

        /// <summary>
        /// Checks if the user exists in the database, based on the combination of the id and username
        /// </summary>
        /// <param name="user">The user with username and id</param>
        /// <returns>True if the user exists, false if not</returns>
        public bool UserExists(User user)
        {
            using (PoliceDbContext db = new PoliceDbContext())
            {
                return db.Users.Any(u => u.Username == user.Username && u.Id == user.Id);
            }
        }

        /// <summary>
        /// Check if the user credentials are valid
        /// </summary>
        /// <param name="user">The user with username and password</param>
        /// <returns>True if valid, false if not valid</returns>
        public bool IsValid(User user)
        {
            using (PoliceDbContext db = new PoliceDbContext())
            {
                return db.Users.Any(u => u.Username == user.Username && u.Password == user.Password);
            }
        }
    }
}