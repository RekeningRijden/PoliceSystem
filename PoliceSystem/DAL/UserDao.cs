using PoliceSystem.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceSystem.DAL
{
    interface UserDao
    {
        void Create(User user);

        bool UserExists(User user);

        User FindById(int id);

        User FindByUsername(string username);

        void Remove(User user);
    }
}
