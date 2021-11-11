using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class CircuitModel
    {
        [Key]
        public int ID { get; set; }
        public string Country { get; set; }
        public string Url { get; set; }
        public string CircuitID { get; set; }
        public string CircuitName { get; set; }
        public float Lat { get; set; }
        public float Long { get; set; }
        public string Locality { get; set; }
    }
}
