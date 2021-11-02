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

namespace DataLayer
{
    public class PopulateDatabase : IPopulateDatabase
    {
        private readonly ApplicationDbContext _context;
        public PopulateDatabase(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Populate()
        {
            await ClearYearDrivers();
            Classes.PopulateClass getDriverData = new(_context);
            await getDriverData.InsertConstrutors(await getDriverData.GetConstructors());
            await getDriverData.LinkDriverToConstructor(await getDriverData.CreateDriver());
            return true;
        }
       public Task<bool> ClearYearDrivers()
        {
            _context.Driver.FromSqlRaw("TRUNCATE driver").ToListAsync();
            _context.SaveChangesAsync();
            return Task.FromResult(true);
        }
    }
}