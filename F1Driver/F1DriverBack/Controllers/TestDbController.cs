using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using F1DriverBack.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace F1Driver_Back.Controllers
{
    [Route("[controller]/[action]")]
    public class TestDbController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TestDbController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("test");
        }
        [HttpPost]
        public IActionResult Test()
        {
            _context.circuit.Add(
                 new ModelLayer.CircuitModel
                 {
                     Country = "germany",
                     Name = "nurburg",
                     Map = "link to map"
                 }
                 );
            var race = new ModelLayer.RaceModel()
            {
                Laps = 58,
                Date = DateTime.Now,
                Circuit = 4
            };
            _context.Add(race);
            _context.SaveChanges();
            //await _context.SaveChangesAsync();

            //filters
            var selectedCircuit = _context.circuit.Where(b => b.Country == "Belgium");
            return Ok(selectedCircuit);
        }
    }
}
