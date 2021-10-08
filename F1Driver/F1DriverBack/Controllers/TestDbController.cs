using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using F1DriverBack.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace F1Driver_Back
{
    public class TestDbController : Controller
    {
        [Route("[controller]")]
        public IActionResult Index(IServiceProvider serviceProvider)
        {
            Console.WriteLine("");
            test(serviceProvider);
            return Ok();
        }
        [HttpPost]
        public void test(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Race.Any())
                {
                    return;
                }
                context.Race.AddRange(
                    new ModelLayer.RaceModel
                    {
                        Year = 1980,
                        Circuit = new ModelLayer.CircuitModel {Country = "Belgium" , Name = "Spa"},
                        Date = DateTime.Now
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
