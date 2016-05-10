using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliceSystem.Models.Domain
{
    public class Theftinfo
    {
        private int Id {get; set;}
        private DateTime LastSeenDate { get; set; }
        private Address LastSeenLocation { get; set; }
        private Car Car { get; set; }
        private DateTime CarFoundDate { get; set; }
        private Address CarFoundLocation { get; set; }
    }
}