using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    class RaceResultModel
    {
        public int ResultID { get; set; }
        public RaceModel Race { get; set; }
        public  DriverModel Driver { get; set; }
        public ConstructorModel Constructor { get; set; }
    }
}
