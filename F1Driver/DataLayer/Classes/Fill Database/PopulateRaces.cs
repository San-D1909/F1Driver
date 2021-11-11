using ModelLayer;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Classes.Fill_Database
{
    class PopulateRaces
    {
        private readonly ApplicationDbContext _context;
        public PopulateRaces(ApplicationDbContext context)
        {
            _context = context;
        }
        public async void RaceHelper()
        {
            List<string> drivers = await GetCurrentDrivers();
            List<RaceModel> rounds = await GetAllRounds();
            List<RaceResultModel> results = new();
            for(int i= 1; i<= rounds.Count();i++)
            {
                if(rounds[i].Date>DateTime.Now)
                {
                    break;
                }
                results.AddRange(await GetAllResultsPerRound(i));
            }
            await InsertRaces(rounds,results);
            return;
        }
        public Task<List<string>> GetCurrentDrivers()
        {
            List<string> drivers = _context.Driver.Where(d => d.DriverID != "").Select(d => d.DriverID).ToList();
            return Task.FromResult(drivers);
        }
        public async Task<List<RaceModel>> GetAllRounds()
        {
            API api = new API { RequestString = "current" };
            JObject parsed = JObject.Parse(await api.SelectJSONFromAPI(api.requestString));
            List<JToken> races = parsed["MRData"]["RaceTable"]["Races"].Children().ToList();
            List<RaceModel> raceModels = new();
            foreach (JToken race in races)
            {
                RaceModel raceModel = race.ToObject<RaceModel>();
                raceModel.CircuitId = (string)race.SelectToken("Circuit.circuitId");
                raceModels.Add(raceModel);
            }
            return raceModels;
        }
        public async Task<List<RaceResultModel>> GetAllResultsPerRound(int round)
        {
            API api = new API { RequestString = "current/" + round + "/results" };
            JObject parsed = JObject.Parse(await api.SelectJSONFromAPI(api.requestString));
            List<JToken> raceResults = parsed["MRData"]["RaceTable"]["Races"][0]["Results"].Children().ToList();
            List<RaceResultModel> raceResultModels = new();
            foreach (JToken result in raceResults)
            {
                RaceResultModel resultModel = result.ToObject<RaceResultModel>();
                resultModel.Race = round.ToString();
                resultModel.DriverID = (string)result.SelectToken("Driver.driverId");
                resultModel.ConstructorID = (string)result.SelectToken("Constructor.constructorId");
                raceResultModels.Add(resultModel);
            }
            return raceResultModels;
        }
        public Task<bool> InsertRaces(List<RaceModel> raceModels,List<RaceResultModel> raceResultModels)
        {
            for (int i = 0; i < raceModels.Count; i++)
            {
                if (_context.Race.Any(s => s.RaceName == raceModels[i].RaceName)) continue;
                _context.Add(raceModels[i]);
                _context.SaveChangesAsync();
            }
            for (int i = 0; i < raceResultModels.Count; i++)
            {
                if (_context.RaceResult.Any(s => s.Race == raceResultModels[i].Race&&s.DriverID==raceResultModels[i].DriverID)) continue;
                
                _context.Add(raceResultModels[i]);
                _context.SaveChangesAsync();
            }
            return Task.FromResult(true);
        }
    }
}
