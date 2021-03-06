using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;

namespace ModelLayer
{
    public class BetModel
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public int Race { get; set; }
        public int First { get; set; }
        public int Second { get; set; }
        public int Third { get; set; }
        public int FastestLap { get; set; }
    }
}
