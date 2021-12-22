using BettingServiceDataLayer.Interfaces;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingServiceDataLayer.Classes
{
    public class BettingClass : IBet
    {
        private readonly ApplicationDbContext _context;
        public BettingClass(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> SetBet(string driver, string category, int userID, int race)
        {
            try
            {
                BetModel bet = new BetModel { DriverID = Convert.ToInt32(driver), Category = Convert.ToInt32(category), UserID = userID, Race = race };
                _context.Bet.Add(bet);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;

            }
        }
    }
}
