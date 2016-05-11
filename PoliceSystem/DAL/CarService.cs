using PoliceSystem.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliceSystem.DAL
{
    public class CarService
    {

        private CarDao carDao = new CarDaoImpl();

        public void Create(Car car)
        {
            using (PoliceDbContext context = new PoliceDbContext())
            {
                carDao.Create(car, context);
            }
        }

        public void Update(Car car)
        {
            using (PoliceDbContext context = new PoliceDbContext())
            {
                carDao.Update(car, context);
            }
        }

        public void FindById(int id)
        {
            using (PoliceDbContext context = new PoliceDbContext())
            {
                carDao.FindById(id, context);
            }
        }

        public void Remove(Car car)
        {
            using (PoliceDbContext context = new PoliceDbContext())
            {
                carDao.Remove(car, context);
            }
        }
    }
}