using PagedList;
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

        public Car FindById(int id)
        {
            using (PoliceDbContext context = new PoliceDbContext())
            {
                return carDao.FindById(id, context);
            }
        }

        public Car FindByLicencePlate(string licencePlate, bool withThefts)
        {
            using (PoliceDbContext context = new PoliceDbContext())
            {
                return carDao.FindByLicencePlate(licencePlate, context, withThefts);
            }
        }

        public bool CarExists(string licencePlate)
        {
            using (PoliceDbContext context = new PoliceDbContext())
            {
                return carDao.CarExists(licencePlate, context);
            }
        }

        public void Remove(Car car)
        {
            using (PoliceDbContext context = new PoliceDbContext())
            {
                carDao.Remove(car, context);
            }
        }

        public IPagedList<Car> GetByPage(int pageNumber, string filter)
        {
            using (PoliceDbContext context = new PoliceDbContext())
            {
                return carDao.GetByPage(pageNumber, filter, context);
            }
        }
    }
}