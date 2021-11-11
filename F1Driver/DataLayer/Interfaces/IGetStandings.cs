using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;

namespace DataLayer.Interfaces
{
    public interface IGetStandings
    {
        public Task<List<StandingModel>> GetCurrentStandings();
    }
}
