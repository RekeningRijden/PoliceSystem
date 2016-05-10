using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliceSystem.Models.Domain
{
    public class Car
    {
        private int Id { get; set; }
        private bool Stolen { get; set; }
        private List<Theftinfo> Thefts { get; set; }
    }
}