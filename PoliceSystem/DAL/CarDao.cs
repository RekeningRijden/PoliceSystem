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

        void FindById(int id, PoliceDbContext context);

        void Delete(Car car, PoliceDbContext context); 
    }
}
