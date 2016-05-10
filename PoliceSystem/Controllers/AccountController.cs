using PoliceSystem.DAL;
using PoliceSystem.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PoliceSystem.Controllers
{
    public class AccountController : Controller
    {
        private UserDao userDao = new UserDaoImpl();

        // GET: Account
        [Authorize]
        public ActionResult Index()
        {
            string username = HttpContext.User.Identity.Name;
            User user = userDao.FindByUsername(username);
            return View(user);
        }

        public ActionResult Login()
        {
            User user = new User();
            return View(user);
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (userDao.UserExists(user))
            {
                //Get user etc.
                FormsAuthentication.SetAuthCookie(user.Username, false);
                return Redirect("/Account/Index");
            }
            else
            {
                ModelState.AddModelError("", "De gebruikersnaam of het wachtwoord is incorrect.");
                return View(user);
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult Register()
        {
            var user = new User();
            return View(user);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Register(User user)
        {
            userDao.Create(user);
            return Redirect("/Home/Index");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Account/Login");
        }
    }
}