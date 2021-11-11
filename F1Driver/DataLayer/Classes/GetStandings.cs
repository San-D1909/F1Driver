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
        public async Task<List<StandingModel>> GetCurrentStandings()
        {
            List<StandingModel> standingModels = await GetDriverStandings();
            return standingModels;
        }
        public Task<List<StandingModel>> GetDriverStandings()
        {
            List<DriverModel> drivers = _context.Driver.Where(d => d.DriverID != "").ToList();
            List<StandingModel> standingModels = new();
            foreach (DriverModel driver in drivers)
            {
                List<RaceResultModel> resultModels = _context.RaceResult.Where(r => r.DriverID == driver.DriverID).ToList();
                StandingModel standingModel = new StandingModel { Driver = driver, Points = 0 };
                foreach(RaceResultModel result in resultModels)
                {
                    standingModel.Points= standingModel.Points + Convert.ToSingle(result.Points);
                    standingModel.Constructor = _context.Constructor.Where(c => c.ConstructorID == result.ConstructorID).First();
                }
                standingModels.Add(standingModel);
            }
            return Task.FromResult(standingModels);
        }
    }
}
