using ModelLayer;
using ModelLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingServiceDataLayer.Interfaces
{
    public interface IBet
    {
        public Task<bool> SetBet(int driver,int category, int userID,int race);
        public Task<BetDTO> GetBet(int userID,int race);
    }
}
