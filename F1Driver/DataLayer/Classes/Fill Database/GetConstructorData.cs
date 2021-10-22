using ModelLayer;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Classes
{
    public class GetConstructorData
    {
        public readonly ApplicationDbContext _context;
        public GetConstructorData(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<bool> InsertConstrutor(List<ConstructorModel> constructors)
        {
/*            foreach (ConstructorModel constructor in constructors)
            {
                _context.Add(constructor);
            }
            _context.SaveChanges();*/
            return Task.FromResult(true);
        }
        public async void GetConstructors()
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
            await InsertConstrutor(constructorModels);
            return;
        }
        public Task<int> GetConstructorID(string ConstructorID)
        {
            ConstructorModel constructor = (ConstructorModel)_context.Constructor.Where(s => s.ConstructorID == ConstructorID);
            return Task.FromResult(constructor.ID);
        }
    }
}
