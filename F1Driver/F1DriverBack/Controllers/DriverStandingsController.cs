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
        private readonly IDriverClass DriverClass;
        public DriverStandingsController(IDriverClass driver)
        {
            DriverClass = driver;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            bool worked = await DriverClass.InsertDriver();
            return Ok(worked);
        }
    }
}
