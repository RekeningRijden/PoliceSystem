using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliceSystem.Models.Domain
{
    public class TrackingPeriod
    {
        [JsonProperty("serialNumber")]
        public int SerialNumber { get; set; }
        [JsonProperty("positions")]
        public List<Position> Positions { get; set; }
        [JsonProperty("startedTracking")]
        public DateTime StartedTracking { get; set; }
        [JsonProperty("finishedTracking")]
        public DateTime FinishedTracking { get; set; }

    }
}