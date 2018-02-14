using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PilotLog.NET.Models
{
    [Table("log")]
    public class Log
    {
        public int ID { get; set; }
        public int userID { get; set; }
        public DateTime date { get; set; }
        public string aircraft_callsign { get; set; }
        public string aircraft_model { get; set; }
        public DateTime takeofftime { get; set; }
        public DateTime landingtime { get; set; }
        public int landings { get; set; }
        public string dep_airport { get; set; }
        public string arr_airport { get; set; }
        public DateTime pic_duration { get; set; }
        public DateTime dual_duration { get; set; }
        public DateTime cop_duration { get; set; }
        public DateTime ifr_duration { get; set; }
        public DateTime night_duration { get; set; }
        public int night_landings { get; set; }
        public string comments { get; set; }
    }
}