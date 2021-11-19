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
            GetImages getImages = new GetImages(_context);
            races = await getImages.GetsCircuitImages(races);
            foreach (RaceModel race in races)
            {
                CircuitModel Circuit = _context.Circuit.Where(c => c.CircuitID == race.CircuitId).First();
                race.FlagUrl = await getImages.GetCountryFlag(Circuit.Country);
                race.Country = Circuit.Country;
            }
            return races;
        }
        public async Task<List<RaceModel>> GetUpcomingRaceDB()
        {//Get upcoming race
            List<RaceModel> races = new();
            races = _context.Race.Where(r => r.Date >= DateTime.Now).ToList();//Adds most recent race to list
            GetImages getImages = new GetImages(_context);
            races = await getImages.GetsCircuitImages(races);//get image of the circuit
            foreach(RaceModel race in races)
            {
                CircuitModel Circuit = _context.Circuit.Where(c => c.CircuitID == race.CircuitId).First();
                race.FlagUrl = await getImages.GetCountryFlag(Circuit.Country);
                race.Country = Circuit.Country;
            }
            return races;
        }
    }
}
