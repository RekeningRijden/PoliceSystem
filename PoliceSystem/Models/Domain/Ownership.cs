using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliceSystem.Models.Domain
{
    public class Ownership
    {
        private int Id { get; set; }
        private DateTime StartDate { get; set; }
        private DateTime EndDate { get; set; }
        private Car Car { get; set; }
        private Driver Driver { get; set; }
    }
}