using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliceSystem.Models.Domain
{
    public class Driver
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("firstName")]
        public string Firstname { get; set; }
        [JsonProperty("lastName")]
        public string Lastname { get; set; }
        [JsonProperty("address")]
        public Address Address { get; set; }
        public List<Ownership> ownerships { get; set; }
    }
}