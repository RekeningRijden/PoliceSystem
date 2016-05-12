using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PoliceSystem.Models.Domain
{
    public class Address
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("street")]
        public string Street { get; set; }
        [JsonProperty("streetNr")]
        public string StreetNr { get; set; }
        [JsonProperty("zipCode")]
        public string ZipCode { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }

        public string GetFullAddress()
        {
            return Street + " "
                + StreetNr + " "
                 + ZipCode + " "
                  + City + " "
                   + Country;
        }
    }
}