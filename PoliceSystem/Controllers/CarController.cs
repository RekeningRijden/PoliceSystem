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
                CarService carService = new CarService();
                CarCalls calCalls = new CarCalls();

                Car car;
                if (carService.CarExists(licencePlate))
                {
                    car = carService.FindByLicencePlate(licencePlate);
                }
                else
                {
                    car = await calCalls.GetCarWithLicencePlate(licencePlate);
                    car.Id = 0;

                    carService.Create(car);
                }

                car = await calCalls.FillCarWithData(car);

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
                Address cfl = carViewModel.Theftinfo.CarFoundLocation;
                car.Thefts.Last().CarFoundLocation.Street = cfl.Street;
                car.Thefts.Last().CarFoundLocation.StreetNr = cfl.StreetNr;
                car.Thefts.Last().CarFoundLocation.ZipCode = cfl.ZipCode;
                car.Thefts.Last().CarFoundLocation.City = cfl.City;
                car.Thefts.Last().CarFoundLocation.Country = cfl.Country;
            }

            new CarService().Update(car);

            return RedirectToAction("Car", "Car", new { licencePlate = car.LicencePlate });
        }

        public async Task<ActionResult> Map()
        {
            CarCalls Carcalls = new CarCalls();
            LocationCalls locationCalls = new LocationCalls();
            Car car = new Car() { LicencePlate = "44-DD-33", CarTrackerId = 9 };
            car.TrackingPeriods = await locationCalls.GetAllTrackingPeriodsFor(car);
            return View(car);
        }
    }
}