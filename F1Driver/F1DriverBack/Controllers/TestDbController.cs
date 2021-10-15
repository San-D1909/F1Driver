using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Datalayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace F1Driver_Back.Controllers
{
    [Route("[controller]/[action]")]
    public class TestDbController : Controller
    {
        private readonly Class1 _class1;
        public TestDbController(ApplicationDbContext context)
        {
            _class1 = new Class1(context);
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("test");
        }
        [HttpPost]
        public async Task<IActionResult> Test()
        {
/*            class1.TestBusi(_context);
            var race = new ModelLayer.RaceModel()
            {
                Laps = 58,
                Date = DateTime.Now,
                Circuit = 4
            };
            _context.Add(race);
            //await _context.SaveChangesAsync();

            //filters
            var selectedCircuit = _context.circuit.Where(b => b.Country == "Belgium");
            var circuits = _context.circuit.Select(b => b.Name);
            _context.SaveChanges();*/
            return Ok(await _class1.TestBusi());
        }
    }
}
