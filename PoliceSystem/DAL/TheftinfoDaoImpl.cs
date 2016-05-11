using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PoliceSystem.Models.Domain;

namespace PoliceSystem.DAL
{
    public class TheftinfoDaoImpl : TheftinfoDao
    {
        public void Create(Theftinfo theftInfo, PoliceDbContext context)
        {
            context.Theftinfos.Add(theftInfo);
            context.SaveChanges();
        }

        public void Update(Theftinfo theftInfo, PoliceDbContext context)
        {
            Theftinfo original = FindById(theftInfo.Id, context);

            context.Entry(original).CurrentValues.SetValues(theftInfo);
            context.SaveChanges();
        }

        public void Remove(Theftinfo theftInfo, PoliceDbContext context)
        {
            context.Theftinfos.Remove(theftInfo);
            context.SaveChanges();
        }

        public Theftinfo FindById(int id, PoliceDbContext context)
        {
            return context.Theftinfos.Find(id);
        }
    }
}