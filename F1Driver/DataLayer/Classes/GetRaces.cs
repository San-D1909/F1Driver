using DataLayer.Interfaces;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Classes.GetWikiImages;

namespace DataLayer.Classes
{
    public class GetRaces : IGetRaces
    {
        private readonly ApplicationDbContext _context;
        public GetRaces(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<RaceModel>> GetSeasonsRacesDB()
        {//Get this seasons races
            int year = DateTime.Now.Year;//use this because of bug with query where it cannot be used directly.
            List<RaceModel> races = _context.Race.Where(r => r.Season == year).ToList();
            races = races.OrderBy(r => r.Date).ToList();//sort the list by date
            GetCircuitImages getCircuitImages = new GetCircuitImages(_context);
            races = await getCircuitImages.GetImages(races);
            return races;
        }
        public async Task<RaceModel> GetUpcomingRaceDB()
        {//Get upcoming race
            List<RaceModel> race = new();
            race.Add(_context.Race.Where(r => r.Date >= DateTime.Now).First());//Adds most recent race to list
            GetCircuitImages getCircuitImages = new GetCircuitImages(_context);
            race = await getCircuitImages.GetImages(race);//get image of the circuit
            return race[0];
        }
    }
}
