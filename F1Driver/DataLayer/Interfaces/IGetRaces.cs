using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IGetRaces
    {
        public Task<List<RaceModel>> GetSeasonsRacesDB();
        public Task<List<RaceModel>> GetUpcomingRaceDB();
    }
}
