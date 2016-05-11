using System;
using System.Collections.Generic;
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
            Car original = FindById(car.Id, context);

            context.Entry(original).CurrentValues.SetValues(car);
            context.SaveChanges();
        }

        public void Remove(Car car, PoliceDbContext context)
        {
            context.Cars.Remove(car);
            context.SaveChanges();
        }

        public Car FindById(int id, PoliceDbContext context)
        {
            return context.Cars.Find(id);
        }
    }
}