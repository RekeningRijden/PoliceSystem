using PoliceSystem.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceSystem.DAL
{
    interface TheftinfoDao
    {
        void Create(Theftinfo theftInfo, PoliceDbContext context);

        void Update(Theftinfo theftInfo, PoliceDbContext context);

        void Remove(Theftinfo theftInfo, PoliceDbContext context);

        Theftinfo FindById(int id, PoliceDbContext context);
    }
}
