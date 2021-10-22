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
    public class DriverClass : IDriverClass
    {
        private readonly ApplicationDbContext _context;
        public DriverClass(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> InsertDriver()
        {
            Classes.GetDriverData getDriverData = new(_context);
            List<DriverModel> driverModels = await getDriverData.CreateDriver();
            foreach(DriverModel driver in driverModels)
            {
                _context.Add(driver);
            }
            int results = _context.SaveChanges();
            if (results > 0) { return true; }
            else { return false; }
        }
       
    }
}