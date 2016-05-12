using PoliceSystem.DAL;
using PoliceSystem.Models.Domain;
using PoliceSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using PoliceSystem.Api;

namespace PoliceSystem.Controllers
{
    public class CarController : Controller
    {

        public ActionResult CarOverview()
        {
            Car car = new Car();
            return View(new CarViewModel(car));
        }

        // GET: Car
        public async Task<ActionResult> Car(string licencePlate)
        {
            if (licencePlate == null || licencePlate.Equals(""))
            {
                return Redirect("CarOverview");
            }
            else
            {
                Car car = new CarService().FindByLicencePlate(licencePlate);
                car = await new CarCalls().GetAllDataFromCar(car);

                return View(new CarViewModel(car));
            }
        }

        [HttpPost]
        public ActionResult CarOverview(CarViewModel carViewModel)
        {
            return RedirectToAction("Car", "Car", new { licencePlate = carViewModel.Car.LicencePlate });
        }

        [HttpPost]
        public ActionResult Car(CarViewModel carViewModel)
        {

            Car car = new CarService().FindById(carViewModel.Car.Id);
            car.Stolen = !car.Stolen;

            if (car.Stolen)
            {
                car.Thefts.Add(carViewModel.Theftinfo);
            }
            else
            {
                Address lsl = car.Thefts.Last().CarFoundLocation;
                lsl.Street = carViewModel.Theftinfo.CarFoundLocation.Street;
                lsl.StreetNr = carViewModel.Theftinfo.CarFoundLocation.StreetNr;
                lsl.ZipCode = carViewModel.Theftinfo.CarFoundLocation.ZipCode;
                lsl.City = carViewModel.Theftinfo.CarFoundLocation.City;
                lsl.Country = carViewModel.Theftinfo.CarFoundLocation.Country;
            }

            new CarService().Update(car);

            return RedirectToAction("Car", "Car", new { licencePlate = car.LicencePlate });
        }
    }
}