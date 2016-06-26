using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoliceSystem.Controllers;
using PoliceSystem.DAL;
using PoliceSystem.Models.Domain;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class UnitTestUserPersistence
    {

        private UserService userService;

        [TestInitialize]
        public void init()
        {
            userService = new UserService();
        }

        [TestMethod]
        public void TestAddUser()
        {
            userService.Create(new User() { Username = "test@test.nl", Password = "test" });
            User user = userService.FindByUsername("test@test.nl");
            Assert.IsNotNull(user);
            Assert.AreEqual("test@test.nl", user.Username);
            Assert.AreEqual("test", user.Password);
            Assert.AreEqual("default", user.UserGroup.Name);

            try
            {
                userService.Create(user);
                Assert.Fail("Could add same user twice");
            } catch(Exception ex)
            {
                Assert.IsTrue(true);
            }

            List<User> users = new List<User>();
            users.Add(user);

            cleanUp(users);
        }

        [TestMethod]
        public void TestUpdateUser()
        {
            userService.Create(new User() { Username = "eric@test.nl", Password = "test"});
            User user = userService.FindByUsername("eric@test.nl");

            Assert.AreEqual("eric@test.nl", user.Username);

            user.Username = "newUsername@test.nl";
            userService.Update(user);
            user = userService.FindById(user.Id);

            Assert.AreEqual("newUsername@test.nl", user.Username);

            List<User> users = new List<User>();
            users.Add(user);

            cleanUp(users); 
        }

        [TestMethod]
        public void TestDeleteUser()
        {
            userService.Create(new User() { Username = "test@test.nl", Password = "test"});
            User user = userService.FindByUsername("test@test.nl");

            //Check if the user exists.
            Assert.IsNotNull(user);

            userService.Remove(user.Id);

            User deletedUser = userService.FindById(user.Id);

            Assert.IsNull(deletedUser);
        }

        [TestMethod]
        public void TestUserExists()
        {
            userService.Create(new User() { Username = "test@test.nl", Password = "test" });
            User user = userService.FindByUsername("test@test.nl");

            Assert.IsTrue(userService.UserExists(user));

            List<User> users = new List<User>();
            users.Add(user);

            cleanUp(users);
        }

        [TestMethod]
        public void TestGetAllUsers()
        {
            int startingAmountOfUsers = userService.GetAllUsers().Count;

            userService.Create(new User() { Username = "test@test.nl", Password = "test" });
            User user = userService.FindByUsername("test@test.nl");
            Assert.AreEqual(1, userService.GetAllUsers().Count - startingAmountOfUsers);

            userService.Create(new User() { Username = "test2@test.nl", Password = "test" });
            User user2 = userService.FindByUsername("test2@test.nl");

            userService.Create(new User() { Username = "test3@test.nl", Password = "test" });
            User user3 = userService.FindByUsername("test3@test.nl");

            Assert.AreEqual(3, userService.GetAllUsers().Count - startingAmountOfUsers);

            try
            {
                userService.Create(new User() { Username = "test@test.nl", Password = "test" });
                Assert.Fail();
            } catch(Exception ex)
            {
                Assert.IsTrue(true);
            }

            Assert.AreEqual(3, userService.GetAllUsers().Count - startingAmountOfUsers);

            List<User> users = new List<User>();
            users.Add(user);
            users.Add(user2);
            users.Add(user3);

            cleanUp(users);
        }

        [TestCleanup]
        public void destroy()
        {
            userService = null;
        }

        public void cleanUp(List<User> users)
        {
            foreach(User user in users)
            {
                userService.Remove(user.Id);
            }
        }
    }
}
