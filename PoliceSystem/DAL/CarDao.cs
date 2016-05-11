using PoliceSystem.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceSystem.DAL
{
    interface CarDao
    {
        void Create(Car car, PoliceDbContext context);

        void Update(Car car, PoliceDbContext context);

        Car FindById(int id, PoliceDbContext context);

        Car FindByLicencePlate(string licencePlate, PoliceDbContext context);

        void Remove(Car car, PoliceDbContext context); 
    }
}
