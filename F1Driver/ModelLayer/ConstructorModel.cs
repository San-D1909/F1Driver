using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{

    public class ConstructorModel
    {
        [Key]
        public int ID { get; set; }
        public string ConstructorID { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public string Url { get; set; }
    }
}
