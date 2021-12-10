using DataLayer;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelLayer;
using System.Threading.Tasks;
using DataLayer.Classes.Fill_Database;
using DataLayer.Interfaces;

namespace DataLayer.Classes
{
    public class PopulateDatabase : IPopulateDatabase
    {
        private readonly ApplicationDbContext _context;
        public PopulateDatabase(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Populate()
        {//Method that calls all the classes to fill the database
                await ClearYear();
            PopulateDriversAndTeams getDriverData = new(_context);
            PopulateCircuits getCircuitData = new(_context);
            PopulateRaces getRaceData = new(_context);
            try
            {
                await getCircuitData.InsertCircuits(await getCircuitData.GetCircuits());
                await getDriverData.InsertConstrutors(await getDriverData.GetConstructors());
                await getDriverData.LinkDriverToConstructor(await getDriverData.CreateDriver());
                getRaceData.RaceHelper();
                return true;
            }
            catch (Exception e)
            {//If the API is down go here.
                Console.WriteLine(e);
                return false;
            }
        }
        public async Task<bool> ClearYear()
        {//drops all drivers so old ones dont stay in the system
            int date = DateTime.Now.Year;
            List<RaceModel> races = await _context.Race.Where(race => race.Season != date).ToListAsync();
            if (races.Count!=0)
            {
                await _context.Driver.FromSqlRaw("TRUNCATE driver").ToListAsync();
                await _context.Circuit.FromSqlRaw("TRUNCATE circuit").ToListAsync();
                await _context.Race.FromSqlRaw("TRUNCATE race").ToListAsync();
                await _context.RaceResult.FromSqlRaw("TRUNCATE raceresult").ToListAsync();
                await _context.Constructor.FromSqlRaw("TRUNCATE constructor").ToListAsync();
                await _context.SaveChangesAsync();
            }
            return true;
        }
    }
}