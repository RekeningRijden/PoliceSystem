using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PoliceSystem.DAL;
using PoliceSystem.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliceSystem.Api
{
    public class JMSHandler
    {

        /// <summary>
        /// Decode a JSON string which contains a Car that has been stolen or found.
        /// Save the car to the database.
        /// </summary>
        /// <param name="json">to decode</param>
        public async void handle(string json)
        {
            System.Diagnostics.Debug.WriteLine(json);

            CarService carService = new CarService();

            var format = "dd-MM-yyyy";
            var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format };

            Car source = JsonConvert.DeserializeObject<Car>(json, dateTimeConverter);
            Car target = carService.FindByLicencePlate(source.LicencePlate, true);
            Boolean carExists = target != null;
            target = carExists ? target : new Car();

            Address address = await new LocationCalls().GetAddress(source.Position);


            if (target.Stolen)
            {
                target.Thefts.Last().CarFoundLocation.Street = address.Street;
                target.Thefts.Last().CarFoundLocation.StreetNr = address.StreetNr;
                target.Thefts.Last().CarFoundLocation.ZipCode = address.ZipCode;
                target.Thefts.Last().CarFoundLocation.City = address.City;
                target.Thefts.Last().CarFoundLocation.Country = address.Country;
                target.Stolen = false;

                carService.Update(target);
            }
            else
            {
                Theftinfo theft = new Theftinfo();
                theft.LastSeenLocation = address;
                theft.LastSeenDate = source.Position.Date;

                target.Thefts.Add(theft);
                target.LicencePlate = source.LicencePlate;
                target.Stolen = true;

                carService.Create(target);
            }
        }
    }
}