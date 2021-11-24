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
    public class GetImages
    {
        private readonly ApplicationDbContext _context;
        public GetImages(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<RaceModel>> GetsCircuitImages(List<RaceModel> races)
        {
            foreach (RaceModel race in races)
            {
                CircuitModel circuit = _context.Circuit.Where(c => c.CircuitID == race.CircuitId).First();
                API api = new API { RequestString = circuit.Url.Remove(0, 29) };//remove unneeded part of the url
                JObject parsed = JObject.Parse(await api.SelectJSONFromAPI(api.requestString));
                List<JToken> imageToken = parsed["query"]["pages"].Children().Children()["thumbnail"]["source"].ToList();//navigate to the image
                if (imageToken.Count() < 1)//check if the url is working
                {
                    api = new API { RequestString = circuit.CircuitID };//url didnt work so now try it with the id of the circuit instead
                    parsed = JObject.Parse(await api.SelectJSONFromAPI(api.requestString));
                    imageToken = parsed["query"]["pages"].Children().Children()["thumbnail"]["source"].ToList();
                }
                    race.ImageUrl = imageToken[0].ToString();//convert jtoken to string
            }
            return races;
        }
        public async Task<List<DriverModel>> GetDriverImages(List<DriverModel> drivers)
        {
            foreach (DriverModel driver in drivers)
            {
                API api = new API { RequestString = driver.Url.Remove(0, 29) };//remove unneeded part of the url
                JObject parsed = JObject.Parse(await api.SelectJSONFromAPI(api.requestString));
                List<JToken> imageToken = parsed["query"]["pages"].Children().Children()["thumbnail"]["source"].ToList();//navigate to the image
                if (imageToken.Count() < 1)//check if the url is working
                {
                    api = new API { RequestString = driver.DriverID };//url didnt work so now try it with the id of the circuit instead
                    parsed = JObject.Parse(await api.SelectJSONFromAPI(api.requestString));
                    imageToken = parsed["query"]["pages"].Children().Children()["thumbnail"]["source"].ToList();
                }
                driver.ImageUrl = imageToken[0].ToString();//convert jtoken to string
            }
            return drivers;
        }
        /*        public async Task<List<ConstructorStandingsModel>> GetConstructorImages(List<ConstructorStandingsModel> constructors)
                {
                    foreach (ConstructorStandingsModel constructor in constructors)
                    {
                        WikiAPIHelper api = new WikiAPIHelper { RequestString = constructor.Constructor.Url.Remove(0, 29) };//remove unneeded part of the url
                        JObject parsed = JObject.Parse(await api.SelectJSONFromAPI(api.requestString));
                        List<JToken> imageToken = parsed["query"]["pages"].Children().Children()["thumbnail"]["source"].ToList();//navigate to the image
                        if (imageToken.Count() < 1)//check if the url is working
                        {
                            api = new WikiAPIHelper { RequestString = constructor.Constructor.ConstructorID };//url didnt work so now try it with the id of the circuit instead
                            parsed = JObject.Parse(await api.SelectJSONFromAPI(api.requestString));
                            imageToken = parsed["query"]["pages"].Children().Children()["thumbnail"]["source"].ToList();
                        }
                        constructor.Constructor.ImageUrl = imageToken[0].ToString();//convert jtoken to string
                    }
                    return constructors;
                }*/
        public async Task<string> GetCountryFlag(string flag)
        {
                API api = new API { RequestString = flag};//remove unneeded part of the url
                JObject parsed = JObject.Parse(await api.SelectJSONFromAPI(api.requestString));
                List<JToken> imageToken = parsed["query"]["pages"].Children().Children()["thumbnail"]["source"].ToList();//navigate to the image
                if (imageToken.Count() < 1)//check if the url is working
                {
                    api = new API { RequestString = flag};//url didnt work so now try it with the id of the circuit instead
                    parsed = JObject.Parse(await api.SelectJSONFromAPI(api.requestString));
                    imageToken = parsed["query"]["pages"].Children().Children()["thumbnail"]["source"].ToList();
                }
                flag = imageToken[0].ToString();//convert jtoken to string
            
            return flag;
        }
    }
}
