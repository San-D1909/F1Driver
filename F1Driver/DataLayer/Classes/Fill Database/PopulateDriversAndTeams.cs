using ModelLayer;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Classes.Fill_Database
{
    public class PopulateDriversAndTeams
    {
        private readonly ApplicationDbContext _context;
        public PopulateDriversAndTeams(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<DriverModel>> CreateDriver()
        {
            API api = new API { RequestString = "current/drivers" };
            JObject parsed = JObject.Parse(await api.SelectJSONFromAPI(api.requestString));
            List<JToken> drivers = parsed["MRData"]["DriverTable"]["Drivers"].Children().ToList();
            List<DriverModel> driverModels = new();
            foreach (JToken driver in drivers)
            {
                DriverModel driverModel = driver.ToObject<DriverModel>();
                driverModels.Add(driverModel);
            }
            return driverModels;
        }
        public async Task<List<DriverModel>> LinkDriverToConstructor(List<DriverModel> driverModels)
        {
            API api = new API { RequestString = "current/last/results" };
            JObject parsed = JObject.Parse(await api.SelectJSONFromAPI(api.requestString));
            List<JToken> drivers = parsed["MRData"]["RaceTable"]["Races"][0]["Results"].Children()["Driver"].ToList();
            Console.WriteLine("");
            List<JToken> constructors = parsed["MRData"]["RaceTable"]["Races"][0]["Results"].Children()["Constructor"].ToList();
            for (int i = 0; i < drivers.Count; i++)
            {
                DriverModel LoopedToModel = drivers[i].ToObject<DriverModel>();
                foreach (DriverModel driver in driverModels)
                {
                    if (driver.DriverID == LoopedToModel.DriverID)
                    {
                        ConstructorModel constructorModel = constructors[i].ToObject<ConstructorModel>();
                        driver.Constructor = await GetConstructorID(constructorModel.ConstructorID);
                        //insert the driver
                        await InsertDriver(driver);
                        break;
                    }
                }
            }
            return driverModels;
        }
        public Task<bool> InsertDriver(DriverModel driver)
        {
            if (_context.Driver.Any(s => s.DriverID == driver.DriverID)) return Task.FromResult(false);
            _context.Add(driver);
            _context.SaveChanges();
            return Task.FromResult(true);
        }
        public Task<int> GetConstructorID(string ConstructorID)
        {
            ConstructorModel constructor = (ConstructorModel)_context.Constructor.First(s => s.ConstructorID == ConstructorID);
            return Task.FromResult(constructor.ID);
        }
        public Task<bool> InsertConstrutors(List<ConstructorModel> constructors)
        {
            for (int i = 0; i < constructors.Count; i++)
            {
                if (_context.Constructor.Any(s => s.ConstructorID == constructors[i].ConstructorID)) continue;
                _context.Add(constructors[i]);
                _context.SaveChangesAsync();
            }
            return Task.FromResult(true);
        }
        public async Task<List<ConstructorModel>> GetConstructors()
        {
            API api = new API { RequestString = "current/constructors" };
            JObject parsed = JObject.Parse(await api.SelectJSONFromAPI(api.requestString));
            List<JToken> teams = parsed["MRData"]["ConstructorTable"]["Constructors"].Children().ToList();
            List<ConstructorModel> constructorModels = new();
            foreach (JToken team in teams)
            {
                ConstructorModel constructorModel = team.ToObject<ConstructorModel>();
                constructorModels.Add(constructorModel);
            }
            return constructorModels;
        }
    }
}
