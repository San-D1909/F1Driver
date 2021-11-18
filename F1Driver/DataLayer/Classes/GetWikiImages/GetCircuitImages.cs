using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using Newtonsoft.Json.Linq;

namespace DataLayer.Classes.GetWikiImages
{
    public class GetCircuitImages
    {
        private readonly ApplicationDbContext _context;
        public GetCircuitImages(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<RaceModel>> GetImages(List<RaceModel> races)
        {
            foreach (RaceModel race in races)
            {
                CircuitModel circuit = _context.Circuit.Where(c => c.CircuitID == race.CircuitId).First();
                circuit.Url = circuit.Url.Remove(0, 29);
                WikiAPIHelper api = new WikiAPIHelper { RequestString = circuit.Url };
                JObject parsed = JObject.Parse(await api.SelectJSONFromAPI(api.requestString));
                List<JToken> imageToken = parsed["query"]["pages"].Children().Children()["thumbnail"]["source"].ToList();
                if (imageToken.Count() < 1)
                {
                    api = new WikiAPIHelper { RequestString = circuit.CircuitID };
                    parsed = JObject.Parse(await api.SelectJSONFromAPI(api.requestString));
                    parsed["query"]["pages"].Children().Children()["thumbnail"]["source"].ToList();
                    race.ImageUrl = imageToken[0].ToString();
                }
                else
                {
                    race.ImageUrl = imageToken[0].ToString();
                }
            }
            return null;
        }
    }
}
