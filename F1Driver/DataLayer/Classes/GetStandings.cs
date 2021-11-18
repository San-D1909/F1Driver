using DataLayer.Interfaces;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Classes
{
    public class GetStandings : IGetStandings
    {
        private readonly ApplicationDbContext _context;
        public GetStandings(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<List<StandingModel>> GetCurrentStandings()
        {//Gets the latest info about the drivers and their constructors
            List<DriverModel> drivers = _context.Driver.Where(d => d.DriverID != "").ToList();
            List<StandingModel> standingModels = new();
            foreach (DriverModel driver in drivers)
            {
                List<RaceResultModel> resultModels = _context.RaceResult.Where(r => r.DriverID == driver.DriverID).ToList();
                StandingModel standingModel = new StandingModel { Driver = driver, Points = 0 };//Set points to 0 before adding the results
                foreach(RaceResultModel result in resultModels)
                {//Loop through all the races to match result with driver, add points to driver after.
                    standingModel.Points= standingModel.Points + Convert.ToSingle(result.Points);
                    standingModel.Constructor = _context.Constructor.Where(c => c.ConstructorID == result.ConstructorID).First();
                }
                standingModels.Add(standingModel);
            }
            return Task.FromResult(standingModels);
        }
    }
}
