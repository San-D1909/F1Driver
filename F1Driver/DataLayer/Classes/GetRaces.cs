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
        {
            //get this seasons
            int year = DateTime.Now.Year;
            List<RaceModel> races = _context.Race.Where(r => r.Season == year).ToList();
            races = races.OrderBy(r => r.Date).ToList();
            return Task.FromResult(races);
        }
        public Task<RaceModel> GetUpcomingRaceDB()
        {
            RaceModel races = _context.Race.Where(r => r.Date >= DateTime.Now).First();
            throw new NotImplementedException();
        }
    }
}
