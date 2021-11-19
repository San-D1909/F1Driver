using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class DriverModel
    {
        [Key]
        public int ID { get; set; }
        public int Constructor { get; set; }
        public string DriverID { get; set; }
        public string Code { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public int RaceAmount { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Url { get; set; }
        [NotMapped]
        public float Points { get; set; }
        [NotMapped]
        public string ImageUrl { get; set; }
    }
}
