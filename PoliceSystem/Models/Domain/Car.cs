using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [JsonProperty("cartrackerId")]
        public int CarTrackerId { get; set; }

        [JsonProperty("licencePlate")]
        public string LicencePlate { get; set; }

        [JsonProperty("stolen")]
        public bool Stolen { get; set; }
        public ICollection<Theftinfo> Thefts { get; set; }

        [NotMapped]
        [JsonProperty("lastPosition")]
        public Position Position { get; set; }

        [NotMapped]
        [JsonProperty("currentOwnership")]
        public Ownership CurrentOwnership { get; set; }
        [NotMapped]
        [JsonProperty("pastOwnerships")]
        public List<Ownership> PastOwnerships { get; set; }

        [NotMapped]
        public List<TrackingPeriod> TrackingPeriods { get; set; }

        public Car()
        {
            this.Thefts = new List<Theftinfo>();
            this.PastOwnerships = new List<Ownership>();
            this.TrackingPeriods = new List<TrackingPeriod>();
        }
    }
}