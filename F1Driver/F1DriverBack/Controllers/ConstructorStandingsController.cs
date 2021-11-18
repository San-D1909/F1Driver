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
        [HttpGet("GetConstructorStandings")]
        public async Task<IActionResult> SendConstructorStandings()
        {
            List<StandingModel> standingModels = await GetStandings.GetCurrentStandings();
            List<ConstructorStandingsModel> Constructors = new();
            for (int i = 0; i < standingModels.Count(); i++)
            {
                standingModels[i].Driver.Points = standingModels[i].Points;
                if (Constructors.Any(a => a.Constructor == standingModels[i].Constructor))
                {
                    Constructors.Find(a => a.Constructor == standingModels[i].Constructor).Drivers.Add(standingModels[i].Driver);
                    Constructors.Find(a => a.Constructor == standingModels[i].Constructor).Points += standingModels[i].Points;
                }
                else
                {
                    List<DriverModel> driverModels = new List<DriverModel> { standingModels[i].Driver };
                    ConstructorStandingsModel standing = new ConstructorStandingsModel { Constructor = standingModels[i].Constructor ,Points = standingModels[i].Points,Drivers = driverModels };
                    Constructors.Add(standing);
                }
            }
            Constructors = Constructors.OrderByDescending(d => d.Points).ToList();
            for (int i = 0; i < Constructors.Count(); i++)
            {
                Constructors[i].Position = i + 1;
            }
            return Ok(Constructors);
        }
    }
}
