using DataLayer.Interfaces;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Classes
{
    public class GetRaces : IGetRaces
    {
        private readonly ApplicationDbContext _context;
        public GetRaces(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<List<RaceModel>> GetSeasonsRacesDB()
        {//Get this seasons races
            int year = DateTime.Now.Year;//use this because of bug with query where it cannot be used directly.
            List<RaceModel> races = _context.Race.Where(r => r.Season == year).ToList();
            races = races.OrderBy(r => r.Date).ToList();//sort the list by date
            return Task.FromResult(races);
        }
        public Task<RaceModel> GetUpcomingRaceDB()
        {//Get upcoming race
            RaceModel race = _context.Race.Where(r => r.Date >= DateTime.Now).First();
            return Task.FromResult(race);
        }
    }
}
