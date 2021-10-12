using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class CircuitModel
    {
        [Key]
        public int CircuitID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Map { get; set; }
    }
}
