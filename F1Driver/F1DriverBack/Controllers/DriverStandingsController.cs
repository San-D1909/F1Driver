using DataLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1DriverBack.Controllers
{
    [Route("[controller]/[action]")]
    public class DriverStandingsController : Controller
    {
        private readonly IPopulateDatabase DriverClass;
        public DriverStandingsController(IPopulateDatabase driver)
        {
            DriverClass = driver;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await DriverClass.Populate();
            return Ok();
        }
    }
}
