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
using System.Net;

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
            if (string.IsNullOrEmpty(licencePlate))
            {
                return Redirect("Index");
            }
            else
            {
                CarService carService = new CarService();
                CarCalls calCalls = new CarCalls();

                Car car;
                string errormessage = "Car with licenceplate: " + licencePlate + " does not exist";
                if (carService.CarExists(licencePlate))
                {
                    car = carService.FindByLicencePlate(licencePlate, true);
                }
                else
                {
                    try
                    {
                        car = await calCalls.GetCarWithLicencePlate(licencePlate);
                    }
                    catch (WebException ex)
                    {
                        car = null;
                        errormessage = ex.Message;
                    }
                    if (car != null)
                    {
                        car.Id = 0;
                        carService.Create(car);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Car", new { errorMessage = errormessage });
                    }
                }
                try
                {
                    car = await calCalls.FillCarWithData(car);
                }
                catch(WebException ex)
                {
                    return RedirectToAction("Index", "Car", new { errorMessage = ex.Message });
                }
                

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
        public ActionResult Map()
        {
            //if (mapViewModel.Car == null)
            //{
            //    mapViewModel.Car = new Car();
            //}
            //else
            //{
            //    try
            //    {
            //        LocationCalls locationCalls = new LocationCalls();
            //        CarCalls carcalls = new CarCalls();
            //        mapViewModel.Car = await carcalls.GetCarWithLicencePlate(mapViewModel.Car.LicencePlate);
            //        mapViewModel.Car.TrackingPeriods = await locationCalls.GetAllTrackingPeriodsFor(mapViewModel.Car);
            //    }
            //    catch (Exception ex)
            //    {
            //        ModelState.AddModelError("", ex.Message);
            //    }
            //}

            //return View(mapViewModel);
            return View();
        }

        [HttpGet]
        public ActionResult GetCarWithLicenceplate(string licenceplate)
        {
            CarService carservice = new CarService();
            Car car = carservice.FindByLicencePlate(licenceplate, false);
            return Json(car, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetLocationsFromCarWithingPeriod(int cartrackerId, string startDate, string endDate)
        {
            LocationCalls locationCalls = new LocationCalls();
            List<TrackingPeriod> trackingPeriods = await locationCalls.GetTrackingPeriodsForCarWithinPeriod(cartrackerId, startDate, endDate);
            return Json(trackingPeriods, JsonRequestBehavior.AllowGet);
        }
    }
}