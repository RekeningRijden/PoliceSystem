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
using PagedList;

namespace PoliceSystem.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        
        [HttpGet]
        public ActionResult Index(string currentFilter, string searchString, int? page, string errorMessage)
        {
            if (!String.IsNullOrEmpty(errorMessage))
            {
                ModelState.AddModelError("", errorMessage);
            }

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            Car car = new Car();

            int pageNumber = page ?? default(int);
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            IPagedList<Car> cars = new CarService().GetByPage(pageNumber, searchString);
            return View(new CarViewModel(car, cars));
        }

        [HttpPost]
        public ActionResult Index(CarViewModel carViewModel)
        {
            return RedirectToAction("Car", "Car", new { licencePlate = carViewModel.Car.LicencePlate });
        }

        // GET: Car
        [HttpGet]
        public async Task<ActionResult> Car(string licencePlate)
        {
            if (licencePlate == null || licencePlate.Trim().Equals(""))
            {
                return Redirect("Index");
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
                    if (car != null)
                    {
                        car.Id = 0;
                        carService.Create(car);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Car", new { errorMessage = "Car with licenceplate: " + licencePlate + " does not exist" });
                    }
                }
                car = await calCalls.FillCarWithData(car);

                return View(new CarViewModel(car));
            }
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
        
        [HttpGet]
        public async Task<ActionResult> Map(Car car)
        {

            LocationCalls locationCalls = new LocationCalls();
            if (car.LicencePlate == null)
            {
                car = new Car() { LicencePlate = "11-22-AA", CarTrackerId = 300000 };
            }
            else
            {
                try
                {
                    CarCalls carcalls = new CarCalls();
                    car = await carcalls.GetCarWithLicencePlate(car.LicencePlate);
                    car.TrackingPeriods = await locationCalls.GetAllTrackingPeriodsFor(car);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Something went wrong. Error: " + ex.Message);
                }
            }

            return View(car);
        }
    }
}