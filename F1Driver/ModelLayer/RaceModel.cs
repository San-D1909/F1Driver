using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class RaceModel
    {
        public int RaceID { get; set; }
        public int Year { get; set; }
        public CircuitModel Circuit{get; set;}
        public int Laps { get; set; }
        public DateTime Date { get; set; }
    }
}
