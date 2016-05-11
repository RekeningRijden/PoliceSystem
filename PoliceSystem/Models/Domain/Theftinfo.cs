using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PoliceSystem.Models.Domain
{
    public class Theftinfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        public DateTime LastSeenDate { get; set; }
        public Address LastSeenLocation { get; set; }
        public Car Car { get; set; }
        public DateTime CarFoundDate { get; set; }
        public Address CarFoundLocation { get; set; }
    }
}