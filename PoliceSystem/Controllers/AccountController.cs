﻿using PoliceSystem.Api;
using PoliceSystem.DAL;
using PoliceSystem.Models.Domain;
using PoliceSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PoliceSystem.Controllers
{
    public class AccountController : Controller
    {
        private UserService userService = new UserService();

        // GET: Account.
        [Authorize]
        public ActionResult Index()
        {
            string username = HttpContext.User.Identity.Name;
            User user = userService.FindByUsername(username);
            return View(user);
        }

        // Get: Login.
        public ActionResult Login()
        {

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            User user = new User();
            return View(user);
        }

        // Post: Login.
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (userService.IsValid(user))
            {
                //Get user etc.
                FormsAuthentication.SetAuthCookie(user.Username, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "De gebruikersnaam of het wachtwoord is incorrect.");
                return View(user);
            }
        }

        [Authorize(Roles="admin")]
        public ActionResult Manage()
        {
            List<User> users = userService.GetAllUsers();
            return View(users);
        }

        // Get: Register, only allowed for admin users.
        [Authorize(Roles = "admin")]
        public ActionResult Register()
        {
            var user = new User();
            return View(user);
        }

        // Post: Register, only allowed for admin users.
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Register(User user)
        {
            try
            {
                userService.Create(user);
                return RedirectToAction("Manage", "Account");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        // Get: Logout.
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Account/Login");
        }

        // Get: Delete, only allowed for admin users.
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id, string errorMessage)
        {
            // If the user could not be deleted from the db in [Post] Delete an error message is set.
            if (errorMessage != null)
            {
                ModelState.AddModelError("", errorMessage);
            }

            // If the user directly navigates to this action without an user id to delete.
            if (id == null)
            {
                ModelState.AddModelError("", "No user selected to delete");
                return Redirect("/Account/Index");
            }
            User user = userService.FindById(id.Value);

            return View(user);

        }

        // Post: Delete, only allowed for admin users.
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(User user)
        {
            try
            {
                userService.Remove(user.Id);
                return RedirectToAction("Manage", "Account");
            }
            catch (InvalidOperationException ex)
            {
                //If the user could not be deleted from the database, the reason is displayed in the [Get] Delete view.
                return RedirectToAction("Delete", new { errorMessage = ex.Message });
            }
        }
    }
}