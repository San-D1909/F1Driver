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
        public int RaceID { get; set; }
        public int Circuit{get; set;}
        public int Laps { get; set; }
        public DateTime Date { get; set; }
    }
}
