using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataLayer;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.InMemory;
using DataLayer.Classes;
using DataLayer.Classes.Fill_Database;
using ModelLayer;

namespace UnitTest
{
    [TestClass]
    public class UnitTestRace
    {
        GetRaces getRaces;
        PopulateRaces populateRaces;
        ApplicationDbContext context;
        DriverModel driverModel = new DriverModel
        {
            Code = "Ver",
            Constructor = 12,
            DriverID = "max_verstappen",
            ID = 1,
            RaceAmount=0
        };
        RaceModel raceModel = new RaceModel
        {
            ID = 17,
            CircuitId="americas",
            Country = "United_States",
            RaceName="United States Grand Prix",
        };
        RaceResultModel raceResultModel = new RaceResultModel
        {
            ID = 1,
            DriverID ="1",
            Laps ="50",
            Points="25",
            Position ="1"
        };
        [TestInitialize]
        public void TestInit()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            context = new ApplicationDbContext(options);
            getRaces = new GetRaces(context);
            populateRaces = new PopulateRaces(context);
        }

        [TestMethod]
        public async Task TestGetUpcomingRaces()
        {
            List<RaceModel> races = await getRaces.GetUpcomingRaceDB();
            Assert.AreEqual(0, races.Count());
        }
        [TestMethod]
        public async Task TestGetSeasonRaces()
        {
            List<RaceModel> races = await getRaces.GetSeasonsRacesDB();
            Assert.AreEqual(0, races.Count());
        }
        [TestMethod]
        public async Task TestInsertRaces()
        {
            List<RaceModel> raceModels = new List<RaceModel> {raceModel};
            List<RaceResultModel> raceResultModels = new List<RaceResultModel> { raceResultModel};
            bool worked = await populateRaces.InsertRaces(raceModels, raceResultModels);
            Assert.IsTrue((await context.Race.Where(r => r.ID!=0).ToListAsync()).Count()>0);
            Assert.IsTrue((await context.RaceResult.Where(r => r.ID!=0).ToListAsync()).Count()>0);
        }
    }
}