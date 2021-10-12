using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1DriverBack.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {           
        }
        public DbSet<ModelLayer.RaceModel> race {get; set;}
        public DbSet<ModelLayer.CircuitModel> circuit { get; set; }
    }
}
