using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PoliceSystem.Models;

namespace PoliceSystem.DAL
{
    public class UserDaoImp : UserDao
    {
        public void Create(User user)
        {
            using (PoliceDbContext db = new PoliceDbContext())
            {
                if (FindByUsername(user.Username) == null) {
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
            throw new NotImplementedException();
        }

        public User FindByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public void Remove(User user)
        {
            throw new NotImplementedException();
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