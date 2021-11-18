using DataLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1DriverBack.Controllers
{
    [Route("[controller]/[action]")]
    public class RacesController : Controller
    {
        private readonly IGetRaces GetRaces;
        public RacesController(IGetRaces getRaces)
        {
            GetRaces = getRaces;
        }
        [HttpGet("SeasonsRaces")]
        public async Task<IActionResult> SendRaces()
        {
            List<RaceModel> races = await GetRaces.GetSeasonsRacesDB();
            return Ok(races);
        }

        [HttpGet("UpcomingRace")]
        public async Task<IActionResult> SendUpcomingRace()
        {
            RaceModel upcomingRace = await GetRaces.GetUpcomingRaceDB();
            return Ok(upcomingRace);
        }
    }
}
