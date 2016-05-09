using PoliceSystem.DAL;
using PoliceSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoliceSystem.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            var user = new User();
            return View(user);
        }

        public ActionResult Register()
        {
            var user = new User();
            return View(user);
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            using (PoliceDbContext db = new PoliceDbContext())
            {
                System.Diagnostics.Debug.WriteLine("Email: " + user.Username);
                System.Diagnostics.Debug.WriteLine("password: " + user.Password);
                bool userExists = db.Users.Any(u => u.Username == user.Username && u.Password == user.Password);
                if (userExists)
                {
                    return Redirect("/Home/Index");
                }
                else
                {
                    ModelState.AddModelError("", "De gebruikersnaam of het wachtwoord is incorrect.");
                    return View(user);
                }
            }
        }



        [HttpPost]
        public ActionResult CreateUser(User user)
        {
            using (PoliceDbContext db = new PoliceDbContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
            return View(user);
        }
    }
}