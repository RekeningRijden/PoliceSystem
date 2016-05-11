using PoliceSystem.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliceSystem.DAL
{
    public class UserService
    {
        private UserDao userDao = new UserDaoImpl();

        public void Create(User user)
        {
            using (PoliceDbContext context = new PoliceDbContext())
            {
                userDao.Create(user, context);
            }
        }

        public void Update(User user)
        {
            using (PoliceDbContext context = new PoliceDbContext())
            {
                userDao.Update(user, context);
            }
        }

        public void Remove(int id)
        {
            using (PoliceDbContext context = new PoliceDbContext())
            {
                userDao.Remove(id, context);
            }
        }

        public User FindById(int id)
        {
            using (PoliceDbContext context = new PoliceDbContext())
            {
                return userDao.FindById(id, context);
            }
        }

        public User FindByUsername(string username)
        {
            using (PoliceDbContext context = new PoliceDbContext())
            {
                return userDao.FindByUsername(username, context);
            }
        }
        
        public bool UserExists(User user)
        {
            using (PoliceDbContext context = new PoliceDbContext())
            {
                return userDao.UserExists(user, context);
            }
        }
        
        public bool IsValid(User user)
        {
            using (PoliceDbContext context = new PoliceDbContext())
            {
                return userDao.IsValid(user, context);
            }
        }

        public List<User> getAllUsers()
        {
            using (PoliceDbContext context = new PoliceDbContext())
            {
                return userDao.getAllUsers(context);
            }
        }
    }
}