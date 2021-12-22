using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingServiceDataLayer.Interfaces
{
    public interface IBet
    {
        public Task<bool> SetBet(string driver,string category, int userID,int race);
    }
}
