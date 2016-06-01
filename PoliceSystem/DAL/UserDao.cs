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
        void Create(User user, PoliceDbContext context);

        void Update(User user, PoliceDbContext context);

        void Remove(int id, PoliceDbContext context);

        User FindById(int id, PoliceDbContext context);

        User FindByUsername(string username, PoliceDbContext context);

        /// <summary>
        /// Checks if the user exists in the database, based on the combination of the id and username.
        /// </summary>
        /// <param name="user">The user with username and id.</param>
        /// <returns>True if the user exists, false if not.</returns>
        bool UserExists(User user, PoliceDbContext context);

        /// <summary>
        /// Check if the user credentials are valid.
        /// </summary>
        /// <param name="user">The user with username and password.</param>
        /// <returns>True if valid, false if not valid.</returns>
        bool IsValid(User user, PoliceDbContext context);

        List<User> getAllUsers(PoliceDbContext context);
    }
}
