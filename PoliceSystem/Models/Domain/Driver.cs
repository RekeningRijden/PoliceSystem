using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliceSystem.Models.Domain
{
    public class Driver
    {
        private int Id { get; set; }
        private string FirstName { get; set; }
        private string Lastname { get; set; }
        private Address Address { get; set; }
        private List<Driver> Driver { get; set; }
    }
}