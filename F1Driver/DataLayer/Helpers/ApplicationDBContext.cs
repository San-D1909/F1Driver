using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<ModelLayer.RaceModel> Race { get; set; }
        public DbSet<ModelLayer.RaceResultModel> RaceResult { get; set; }
        public DbSet<ModelLayer.CircuitModel> Circuit { get; set; }
        public DbSet<ModelLayer.DriverModel> Driver { get; set; }
        public DbSet<ModelLayer.ConstructorModel> Constructor { get; set; }
    }
}

