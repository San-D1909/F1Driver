using DataLayer.Classes.GetWikiImages;
using DataLayer.Interfaces;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Classes
{
    public class GetStandings : IGetStandings
    {
        private readonly ApplicationDbContext _context;
        public GetStandings(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<StandingModel>> GetCurrentStandings()
        {//Gets the latest info about the drivers and their constructors
            List<DriverModel> drivers = _context.Driver.Where(d => d.DriverID != "").ToList();
            GetImages getImages = new GetImages(_context);
            drivers = await getImages.GetDriverImages(drivers);//get image of the circuit
            List<StandingModel> standingModels = new();
            foreach (DriverModel driver in drivers)
            {
                List<RaceResultModel> resultModels = _context.RaceResult.Where(r => r.DriverID == driver.DriverID).ToList();
                StandingModel standingModel = new StandingModel { Driver = driver, Points = 0 };//Set points to 0 before adding the results
                foreach (RaceResultModel result in resultModels)
                {//Loop through all the races to match result with driver, add points to driver after.
                    standingModel.Points = standingModel.Points + Convert.ToSingle(result.Points);
                    standingModel.Constructor = _context.Constructor.Where(c => c.ConstructorID == result.ConstructorID).First();
                }
                standingModels.Add(standingModel);
            }
            return standingModels;
        }
        public async Task<List<ConstructorStandingsModel>> ConstructorStandingsGenerator()
        {//Gets the latest info about the drivers and their constructors
            List<StandingModel> standingModels = await GetCurrentStandings();
            List<ConstructorStandingsModel> Constructors = new();
            for (int i = 0; i < standingModels.Count(); i++)
            {
                standingModels[i].Driver.Points = standingModels[i].Points;
                if (Constructors.Any(a => a.Constructor == standingModels[i].Constructor))//Check if constructor allready exists in the list
                {//Link points and driver to the constructor
                    Constructors.Find(a => a.Constructor == standingModels[i].Constructor).Drivers.Add(standingModels[i].Driver);
                    Constructors.Find(a => a.Constructor == standingModels[i].Constructor).Points += standingModels[i].Points;
                }
                else//The constructor is not in the list so create a new constructor
                {//Link points and driver to the constructor
                    List<DriverModel> driverModels = new List<DriverModel> { standingModels[i].Driver };
                    ConstructorStandingsModel standing = new ConstructorStandingsModel { Constructor = standingModels[i].Constructor, Points = standingModels[i].Points, Drivers = driverModels };
                    Constructors.Add(standing);
                }
            }
/*            GetImages getImages = new GetImages(_context);//Instatiate class
            Constructors = await getImages.GetConstructorImages(Constructors);//get image of the Constructor*/
            Constructors = Constructors.OrderByDescending(d => d.Points).ToList();
            for (int i = 0; i < Constructors.Count(); i++)
            {//Adds Position in competition to constructor
                Constructors[i].Position = i + 1;
            }
            return Constructors;
        }
    }
}
