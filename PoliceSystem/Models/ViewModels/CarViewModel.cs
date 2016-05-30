using PagedList;
using PoliceSystem.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliceSystem.Models.ViewModels
{
    public class CarViewModel
    {
        public Car Car { get; set; }
        public IPagedList<Car> Cars { get; set; }
        public Theftinfo Theftinfo { get; set; }

        public CarViewModel()
        {
            Theftinfo = new Theftinfo();
        }

        public CarViewModel(Car car) : this()
        {
            this.Car = car;
        }

        public CarViewModel(Car car, IPagedList<Car> cars) : this()
        {
            this.Car = car;
            this.Cars = cars;
        }
    }
}