using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class ConstructorStandingsModel
    {
        public List<DriverModel> Drivers { get; set; }
        public ConstructorModel Constructor { get; set; }
        public float Points { get; set; }
        public int Position { get; set; }
    }
}
