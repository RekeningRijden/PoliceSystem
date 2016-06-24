using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliceSystem.Models.Domain
{
    public class Position
    {

        [JsonProperty("date")]
        public DateTime Date { get; set; }
        [JsonProperty("latitude")]
        public decimal Latitude { get; set; }
        [JsonProperty("longitude")]
        public decimal Longitude { get; set; }

        public Position(decimal lat, decimal lng)
        {
            this.Latitude = lat;
            this.Longitude = lng;
        }
    }
}