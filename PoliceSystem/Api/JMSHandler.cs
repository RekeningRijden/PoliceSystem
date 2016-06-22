using Newtonsoft.Json;
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

        public async void handle(string json)
        {
            System.Diagnostics.Debug.WriteLine(json);

            CarService carService = new CarService();

            Car source = JsonConvert.DeserializeObject<Car>(json);
            Car carInDB = carService.FindByLicencePlate(source.LicencePlate, true);
            Boolean carExists = carInDB != null;
            carInDB = carExists ? carInDB : new Car();

            Address address = await new LocationCalls().GetAddress(source.Position);

            if (source.Stolen)
            {
                Theftinfo theft = new Theftinfo();
                theft.LastSeenLocation = address;
                theft.LastSeenDate = source.Position.Date;

                carInDB.Thefts.Add(theft);

                if (carExists)
                {
                    //carService.Update(car);
                }
                else 
                {
                    //carService.Create(car);
                }
            }
        }
    }
}