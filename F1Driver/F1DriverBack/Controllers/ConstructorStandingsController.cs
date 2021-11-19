using DataLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Interfaces;
using ModelLayer;

namespace F1DriverBack.Controllers
{
    [Route("[controller]/[action]")]
    public class ConstructorStandingsController : Controller
    {
        private readonly IGetStandings GetStandings;
        public ConstructorStandingsController(IGetStandings getStandings)
        {
            GetStandings = getStandings;
        }
        [HttpGet("ConstructorStandings")]
        public async Task<IActionResult> SendConstructorStandings()
        {
            List<ConstructorStandingsModel> Constructors = await GetStandings.ConstructorStandingsGenerator();
            return Ok(Constructors);
        }
    }
}
