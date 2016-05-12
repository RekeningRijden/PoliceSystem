using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PoliceSystem.Models.Domain;


namespace PoliceSystem.DAL
{
    public class CarDaoImpl : CarDao
    {
        public void Create(Car car, PoliceDbContext context)
        {
            context.Cars.Add(car);
            context.SaveChanges();
        }

        public void Update(Car car, PoliceDbContext context)
        {
            foreach (Theftinfo t in car.Thefts)
            {
                if (t.LastSeenLocation.Id == 0) { context.Addresses.Add(t.LastSeenLocation); }
                if (t.CarFoundLocation.Id == 0) { context.Addresses.Add(t.CarFoundLocation); }
            }

            context.Cars.Attach(car);

            context.Entry(car).State = EntityState.Modified;

            car.Thefts.Where(x => x.Id == 0).ToList().ForEach(x => context.Entry(x).State = EntityState.Added);
            car.Thefts.Where(x => x.Id != 0).ToList().ForEach(x => context.Entry(x).State = EntityState.Modified);

            context.SaveChanges();
        }

        public Car FindById(int id, PoliceDbContext context)
        {
            return context.Cars.Include(c => c.Thefts.Select(t => t.LastSeenLocation)).Single(c => c.Id == id);
        }

        public Car FindByLicencePlate(string licencePlate, PoliceDbContext context)
        {
            return context.Cars.Include(c => c.Thefts.Select(t => t.LastSeenLocation)).Single(c => c.LicencePlate == licencePlate);
        }

        public bool CarExists(string licencePlate, PoliceDbContext context)
        {
            return context.Cars.Any(c => c.LicencePlate == licencePlate);
        }

        public void Remove(Car car, PoliceDbContext context)
        {
            context.Cars.Remove(car);
            context.SaveChanges();
        }
    }
}