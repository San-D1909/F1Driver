using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class RaceModel
    {
        [Key]
        public int ID { get; set; }
        public int CircuitID{get; set;}
        public int Round { get; set; }
        public int Laps { get; set; }
        public DateTime Date { get; set; }
    }
}
