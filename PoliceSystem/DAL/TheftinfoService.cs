using PoliceSystem.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliceSystem.DAL
{
    public class TheftinfoService
    {
        private TheftinfoDao theftInfoDao = new TheftinfoDaoImpl();

        public void Create(Theftinfo theftInfo)
        {
            using (PoliceDbContext context = new PoliceDbContext())
            {
                theftInfoDao.Create(theftInfo, context);
            }
        }

        public void Update(Theftinfo theftInfo)
        {
            using (PoliceDbContext context = new PoliceDbContext())
            {
                theftInfoDao.Update(theftInfo, context);
            }
        }

        public void Remove(Theftinfo theftInfo)
        {
            using (PoliceDbContext context = new PoliceDbContext())
            {
                theftInfoDao.Remove(theftInfo, context);
            }
        }

        public void FindById(int id)
        {
            using (PoliceDbContext context = new PoliceDbContext())
            {
                theftInfoDao.FindById(id, context);
            }
        }
    }
}