using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;

namespace ModelLayer.DTO
{
    public class BetDTO
    {
        [Key]
        public int ID { get; set; }
        public DriverModel Driver { get; set; }
        public int UserID { get; set; }
        public RaceModel Race { get; set; }
        public int Category { get; set; }
    }
}
