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
            await ClearYearDrivers();
            PopulateDriversAndTeams getDriverData = new(_context);
            PopulateCircuits getCircuitData = new(_context);
            PopulateRaces getRaceData = new(_context);
             await getCircuitData.InsertCircuits(await getCircuitData.GetCircuits());
            await getDriverData.InsertConstrutors(await getDriverData.GetConstructors());
            await getDriverData.LinkDriverToConstructor(await getDriverData.CreateDriver());
            getRaceData.RaceHelper();
            return true;
        }
       public Task<bool> ClearYearDrivers()
        {//drops all drivers so old ones dont stay in the system
            _context.Driver.FromSqlRaw("TRUNCATE driver").ToListAsync();
            _context.Constructor.FromSqlRaw("TRUNCATE constructor").ToListAsync();
            _context.SaveChangesAsync();
            return Task.FromResult(true);
        }
    }
}