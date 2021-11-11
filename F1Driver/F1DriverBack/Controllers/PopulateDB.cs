using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DataLayer.Interfaces;

namespace F1DriverBack.Controllers
{
    [Route("[controller]/[action]")]
    public class PopulateDB : Controller
    {
        private readonly IPopulateDatabase DriverClass;
        public PopulateDB(IPopulateDatabase driverClass)
        {
            DriverClass = driverClass;
        }
        [HttpGet]
        public async Task<IActionResult> PopulateDb()
        {
            await DriverClass.Populate();
            return Ok();
        }
    }
}
