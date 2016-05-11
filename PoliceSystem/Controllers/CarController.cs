using PoliceSystem.DAL;
using PoliceSystem.Models.Domain;
using PoliceSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoliceSystem.Controllers
{
    public class CarController : Controller
    {
        // GET: Car
        public ActionResult Car(string licencePlate)
        {
            CarService carService = new CarService();
            Car car = carService.FindByLicencePlate(licencePlate);

            return View(car);
        }

        public ActionResult CarOverview()
        {
            Car car = new Car();
            return View(new CarViewModel(car));
        }

        [HttpPost]
        public ActionResult CarOverview(CarViewModel carViewModel)
        {
            return RedirectToAction("Car", "Car", new { licencePlate = carViewModel.Car.LicencePlate });
        }
    }
}