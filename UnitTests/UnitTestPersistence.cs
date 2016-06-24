using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoliceSystem.Controllers;
using PoliceSystem.DAL;
using PoliceSystem.Models.Domain;

namespace UnitTests
{
    [TestClass]
    public class UnitTestPersistence
    {
        [TestMethod]
        public void TestMethodAddUser()
        {
            UserService userService = new UserService();
            userService.Create(new User() { Username = "test@test.nl", Password = "test" });
            User user = userService.FindByUsername("test@test.nl");
            Assert.IsNotNull(user);
            Assert.AreEqual(user.Username, "test@test.nl");
        }
    }
}
