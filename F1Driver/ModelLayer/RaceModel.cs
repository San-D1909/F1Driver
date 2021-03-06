using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class RaceModel
    {
        [Key]
        public int ID { get; set; }
        public string RaceName{get; set;}
        public string CircuitId { get; set; }
        public int Season { get; set; }
        public int Round { get; set; }
        public string Url { get; set; }
        public DateTime Date { get; set; }
        [NotMapped]
        public string ImageUrl { get; set; }
        [NotMapped]
        public string FlagUrl { get; set; }
        [NotMapped]
        public string Country { get; set; }
    }
}
