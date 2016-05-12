using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PoliceSystem.Models.Domain
{
    public class Theftinfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime LastSeenDate { get; set; }
        public Address LastSeenLocation { get; set; }
        public Car Car { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime CarFoundDate { get; set; }
        public Address CarFoundLocation { get; set; }

        public Theftinfo()
        {
            this.LastSeenDate = DateTime.Now;
            this.CarFoundDate = DateTime.Now;

            this.LastSeenLocation = new Address();
            this.CarFoundLocation = new Address();
        }
    }
}