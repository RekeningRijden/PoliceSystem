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
        private UserDao userDao = new UserDaoImpl();
        
        public ActionResult Index()
        {
            string username = HttpContext.User.Identity.Name;
            User user = userDao.FindByUsername(username);
            return View(user);
        }
    }
}