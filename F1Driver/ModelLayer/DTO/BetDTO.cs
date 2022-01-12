using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class BetDTO
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public int Race { get; set; }
        public DriverModel First { get; set; }
        public DriverModel Second { get; set; }
        public DriverModel Third { get; set; }
        public DriverModel FastestLap { get; set; }
    }
}
