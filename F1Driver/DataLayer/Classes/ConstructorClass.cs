using Microsoft.EntityFrameworkCore;
using ModelLayer;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Interfaces;

namespace DataLayer.Classes
{
    public class ConstructorClass : IConstructorClass
    {
        private readonly ApplicationDbContext _context;
        public ConstructorClass(ApplicationDbContext context)
        {
            _context = context;
        }

    }
}
