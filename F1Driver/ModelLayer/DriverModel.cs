using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    class DriverModel
    {
        public int DriverID { get; set; }
        public ConstructorModel Constructor { get; set; }
        public string SystemName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RaceAmount { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string ImageURL { get; set; }
    }
}
