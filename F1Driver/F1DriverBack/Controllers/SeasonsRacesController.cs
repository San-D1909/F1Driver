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
    public class SeasonsRacesController : Controller
    {
        private readonly IGetRaces GetRaces;
        public SeasonsRacesController(IGetRaces getRaces)
        {
            GetRaces = getRaces;
        }
        [HttpGet("GetSeasonsRaces")]
        public async Task<IActionResult> SendRaces()
        {
            List<RaceModel> races = await GetRaces.GetSeasonsRacesDB();
            return Ok(races);
        }
    }
}
