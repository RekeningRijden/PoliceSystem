using PoliceSystem.DAL;
using PoliceSystem.Models;
using PoliceSystem.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoliceSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private UserService userService = new UserService();
        
        public ActionResult Index()
        {
            string username = HttpContext.User.Identity.Name;
            User user = userService.FindByUsername(username);
            return View(user);
        }

        public ActionResult CarOverview()
        {
            return View();
        }
    }
}