using ModelLayer;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Classes
{
    public class GetDriverData
    {
        private readonly ApplicationDbContext _context;
        public GetDriverData(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<DriverModel>> CreateDriver()
        {
            GetConstructorData getConstructorData = new(_context);
            getConstructorData.GetConstructors();
            API api = new API { RequestString = "current/drivers" };
            JObject parsed = JObject.Parse(await api.SelectJSONFromAPI(api.requestString));
            List<JToken> drivers = parsed["MRData"]["DriverTable"]["Drivers"].Children().ToList();
            List<DriverModel> driverModels = new();
            foreach (JToken driver in drivers)
            {
                DriverModel driverModel = driver.ToObject<DriverModel>();
                driverModels.Add(driverModel);
            }
            driverModels = await LinkDriverToConstructor(driverModels);
            return driverModels;
        }
        public async Task<List<DriverModel>> LinkDriverToConstructor(List<DriverModel> driverModels)
        {
            GetConstructorData getConstructorData = new(_context);
            API api = new API { RequestString = "current/last/results" };
            JObject parsed = JObject.Parse(await api.SelectJSONFromAPI(api.requestString));
            List<JToken> drivers = parsed["MRData"]["RaceTable"]["Races"][0]["Results"].Children()["Driver"].ToList();
            Console.WriteLine("");
            List<JToken> constructors = parsed["MRData"]["RaceTable"]["Races"][0]["Results"].Children()["Constructor"].ToList();
            for (int i = 0; i <= drivers.Count; i++)
            {
                DriverModel LoopedToModel = drivers[i].ToObject<DriverModel>();
                foreach (DriverModel driver in driverModels)
                {
                    if (driver.DriverID == LoopedToModel.DriverID)
                    {
                        ConstructorModel constructorModel = constructors[i].ToObject<ConstructorModel>();
                        driver.Constructor = await getConstructorData.GetConstructorID(constructorModel.ConstructorID);
                    }
                }
            }
            return driverModels;
        }
    }
}
