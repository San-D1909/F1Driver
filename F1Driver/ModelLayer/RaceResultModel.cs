using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class RaceResultModel
    {
        [Key]
        public int ID { get; set; }
        public string Points { get; set; }
        public string Position { get; set; }
        public string Grid { get; set; }
        public string Laps { get; set; }
        public string Status { get; set; }
        public string Race { get; set; }
        public string DriverID { get; set; }
        public string ConstructorID { get; set; }
        public string FastestLapRank { get; set; }
    }
}
