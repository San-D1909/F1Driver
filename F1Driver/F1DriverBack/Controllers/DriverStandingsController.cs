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
        [HttpGet("DriverStandings")]
        public async Task<IActionResult> SendDriverStandings()
        {
            List<StandingModel> standingModels = await GetStandings.GetCurrentStandings();
            standingModels = standingModels.OrderByDescending(d => d.Points).ToList();
            for (int i = 0; i < standingModels.Count(); i++)
            {
                standingModels[i].Position = i+1;
            }
            return Ok(standingModels);
        }
    }
}
