using PoliceSystem.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PoliceSystem.Models.ViewModels
{
    public class MapViewModel
    {

        public Car Car { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime Start { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime End { get; set; }

        public MapViewModel()
        {
            this.Start = DateTime.Now;
            this.End = DateTime.Now;
        }

        public MapViewModel(Car car) : base()
        { 
            this.Car = car;
        }

    }
}