using PoliceSystem.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceSystem.DAL
{
    interface UserGroupDao
    {
        /// <summary>
        /// Add the given UserGroup as new record to the database.
        /// </summary>
        /// <param name="userGroup">to add to the database</param>
        void Create(UserGroup userGroup);

        /// <summary>
        /// Find a UserGroup in the database by its Id.
        /// </summary>
        /// <param name="id">the Id to check for</param>
        /// <returns>The found UserGroup</returns>
        UserGroup FindById(int id);

        /// <summary>
        /// Find a UserGroup in the database by its Name.
        /// </summary>
        /// <param name="name">the Name to check for</param>
        /// <returns>The found UserGroup</returns>
        UserGroup FindByName(string name);

        /// <summary>
        /// Remove a UserGroup from the database.
        /// </summary>
        /// <param name="userGroup">The usergroup to remove</param>
        void Remove(UserGroup userGroup);

        /// <summary>
        /// Add a User to a UserGroup.
        /// </summary>
        /// <param name="userGroup">to add the User to</param>
        /// <param name="user">to add to the UserGroup</param>
        void AddToUserGroup(UserGroup userGroup, User user);
    }
}
