using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PoliceSystem.Models.Domain
{
    public class Car
    {
        [JsonProperty("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [JsonProperty("licencePlate")]
        public string LicencePlate { get; set; }

        public bool Stolen { get; set; }
        public List<Theftinfo> Thefts { get; set; }

        [NotMapped]
        [JsonProperty("currentOwnership")]
        public Ownership CurrentOwnership { get; set; }
        [NotMapped]
        [JsonProperty("pastOwnerships")]
        public List<Ownership> PastOwnerships { get; set; }

        public Car()
        {
            this.Thefts = new List<Theftinfo>();
        }
    }
}