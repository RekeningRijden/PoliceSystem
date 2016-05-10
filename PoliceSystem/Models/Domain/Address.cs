using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliceSystem.Models.Domain
{
    public class Address
    {
        private int Id { get; set; }
        private string Street { get; set; }
        private string StreetNr { get; set; }
        private string ZipCode { get; set; }
        private string City { get; set; }
        private string Country { get; set; }
    }
}