using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelLayer;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DataLayer.Classes.Fill_Database
{
    class PopulateCircuits
    {
        private readonly ApplicationDbContext _context;
        public PopulateCircuits(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<CircuitModel>> GetCircuits()
        {
            API api = new API { F1RequestString = "current/circuits" };
            JObject parsed = JObject.Parse(await api.SelectJSONFromAPI(api.requestString));
            List<JToken> circuits = parsed["MRData"]["CircuitTable"]["Circuits"].Children().ToList();
            List<CircuitModel> circuitModels = new();
            foreach (JToken circuit in circuits)
            {
                CircuitModel circuitModel = circuit.ToObject<CircuitModel>();
                circuitModel.Long = (float)circuit.SelectToken("Location.long");
                circuitModel.Lat = (float)circuit.SelectToken("Location.lat");
                circuitModel.Country = (string)circuit.SelectToken("Location.country");
                circuitModel.Locality = (string)circuit.SelectToken("Location.locality");
                circuitModels.Add(circuitModel);
            }
            return circuitModels;
        }
        public Task<bool> InsertCircuits(List<CircuitModel> circuitModels)
        {
            for (int i = 0; i < circuitModels.Count; i++)
            {
                if (_context.Circuit.Any(s => s.CircuitID == circuitModels[i].CircuitID)) continue;
                _context.Add(circuitModels[i]);
                _context.SaveChangesAsync();
            }
            return Task.FromResult(true);
        }
    }
}
