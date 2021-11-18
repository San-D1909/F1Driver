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
                WikiAPIHelper api = new WikiAPIHelper { RequestString = circuit.Url.Remove(0, 29)};//remove unneeded part of the url
                JObject parsed = JObject.Parse(await api.SelectJSONFromAPI(api.requestString));
                List<JToken> imageToken = parsed["query"]["pages"].Children().Children()["thumbnail"]["source"].ToList();//navigate to the image
                if (imageToken.Count() < 1)//check if the url is working
                {
                    api = new WikiAPIHelper { RequestString = circuit.CircuitID };//url didnt work so now try it with the id of the circuit instead
                    parsed = JObject.Parse(await api.SelectJSONFromAPI(api.requestString));
                    imageToken = parsed["query"]["pages"].Children().Children()["thumbnail"]["source"].ToList();
                    race.ImageUrl = imageToken[0].ToString();//convert jtoken to string
                }
                else//url works and image is received
                {
                    race.ImageUrl = imageToken[0].ToString();//convert jtoken to string
                }  
            }
            return races;
        }
    }
}
