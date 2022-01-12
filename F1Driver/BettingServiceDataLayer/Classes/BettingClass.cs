using BettingServiceDataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using ModelLayer;
using ModelLayer.DTO;
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

        public async Task<BetDTO> GetBet(int userID, int race)
        {
            try
            {
                BetModel bet = await _context.Bet.Where(b => b.Race == race && b.UserID == userID).FirstAsync();
                BetDTO betDTO = new BetDTO { ID = bet.ID, Race = bet.Race, UserID = bet.UserID, First = await GetDriver(bet.First), Second = await GetDriver(bet.Second), Third = await GetDriver(bet.Third), FastestLap = await GetDriver(bet.FastestLap) };
                return betDTO;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        private async Task<DriverModel> GetDriver(int driverID)
        {
            return await _context.Driver.Where(d => d.ID == driverID).FirstAsync();
        }
        public async Task<bool> SetBet(int driver, int category, int userID, int race)
        {
            if (_context.Bet.Any(b => b.Race == race && b.UserID == userID))
            {
                BetModel existingBet = await _context.Bet.Where(b => b.Race == race && b.UserID == userID).FirstAsync();
                try
                {
                    switch (category)
                    {
                        case 1:
                            if (existingBet.First == driver)
                            {
                                return false;
                            }
                            existingBet.First = driver;
                            await _context.SaveChangesAsync();
                            break;
                        case 2:
                            if (existingBet.Second == driver)
                            {
                                return false;
                            }
                            existingBet.Second = driver;
                            await _context.SaveChangesAsync();
                            break;
                        case 3:
                            if (existingBet.Third == driver)
                            {
                                return false;
                            }
                            existingBet.Third = driver;
                            await _context.SaveChangesAsync();
                            break;
                        case 4:
                            if (existingBet.FastestLap == driver)
                            {
                                return false;
                            }
                            existingBet.FastestLap = driver;
                            await _context.SaveChangesAsync();
                            break;
                    }
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
            BetModel bet = new BetModel { UserID = userID, Race = race };
            switch (category)
            {
                case 1:
                    bet.First = driver;
                    break;
                case 2:
                    bet.Second = driver;
                    break;
                case 3:
                    bet.Third = driver;
                    break;
                case 4:
                    bet.FastestLap = driver;
                    break;
            }
            _context.Bet.Add(bet);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
