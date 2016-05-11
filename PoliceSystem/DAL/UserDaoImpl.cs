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

        public void Create(User user, PoliceDbContext context)
        {
            if (FindByUsername(user.Username, context) == null)
            {
                UserGroup userGroup = userGroupDao.FindByName("default");
                context.UserGroups.Attach(userGroup);
                user.UserGroup = userGroup;
                context.Users.Add(user);
                context.SaveChanges();
            }
            else
            {
                //laat de gebruiker weten dat de username bezet is
                throw new InvalidOperationException("Username already exists");
            }
        }
        
        public void Update(User user, PoliceDbContext context)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id, PoliceDbContext context)
        {
            User user = FindById(id, context);
            if (UserExists(user, context))
            {
                if (user.UserGroup.Name.Equals("admin"))
                {
                    throw new InvalidOperationException("Cannot delete admin account");
                }

                //context.Users.Attach(user);
                context.Users.Remove(user);
                context.SaveChanges();
            }

            else
            {
                //User doesnt exists.
                throw new InvalidOperationException("User with username: " + user.Username + " does not exist");
            }
        }

        public User FindById(int id, PoliceDbContext context)
        {
            try
            {
                return context.Users.Include(u => u.UserGroup).Single(u => u.Id == id);
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

        public User FindByUsername(string username, PoliceDbContext context)
        {
            try
            {
                return context.Users.Include(u => u.UserGroup).Single(u => u.Username == username);
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

        public bool UserExists(User user, PoliceDbContext context)
        {
            return context.Users.Any(u => u.Username == user.Username && u.Id == user.Id);
        }

        public bool IsValid(User user, PoliceDbContext context)
        {
            return context.Users.Any(u => u.Username == user.Username && u.Password == user.Password);
        }

        public List<User> getAllUsers(PoliceDbContext context)
        {
            return context.Users.Include(u => u.UserGroup).ToList();
        }
    }
}