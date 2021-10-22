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
        public int ResultID { get; set; }
        public RaceModel Race { get; set; }
        public  DriverModel Driver { get; set; }
        public ConstructorModel Constructor { get; set; }
    }
}
