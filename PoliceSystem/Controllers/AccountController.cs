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
        private UserDao userDao = new UserDaoImp();

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
            if (userDao.UserExists(user))
            {
                //Get user etc.
                return Redirect("/Home/Index");
            }
            else
            {
                ModelState.AddModelError("", "De gebruikersnaam of het wachtwoord is incorrect.");
                return View(user);
            }
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            
            return View(user);
        }
    }
}