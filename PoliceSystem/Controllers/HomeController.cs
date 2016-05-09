using PoliceSystem.DAL;
using PoliceSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoliceSystem.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            var loginViewModel = new LoginViewModel();
            return View(loginViewModel);
        }
        
        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            using (PoliceDbContext db = new PoliceDbContext())
            {
                System.Diagnostics.Debug.WriteLine("Email: " + loginViewModel.Email);
                System.Diagnostics.Debug.WriteLine("password: " + loginViewModel.Password);
                bool userExists = db.Users.Any(user => user.Username == loginViewModel.Email && user.Password == loginViewModel.Password);
                if(userExists)
                {
                    return Redirect("/Home/Index");
                }
                else
                {
                    ModelState.AddModelError("", "De gebruikersnaam of het wachtwoord is incorrect.");
                    return View(loginViewModel);
                }
            }
        }
    }
}