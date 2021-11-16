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
    public class DriverStandingsController : Controller
    {
        private readonly IGetStandings GetStandings;
        public DriverStandingsController(IGetStandings getStandings)
        {
            GetStandings = getStandings;
        }
        [HttpGet("GetDriverStandings")]
        public async Task<IActionResult> SendDriverStandings()
        {
            List<StandingModel> standingModels = await GetStandings.GetCurrentStandings();
            return Ok(standingModels);
        }
    }
}
