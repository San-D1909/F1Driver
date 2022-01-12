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


namespace UnitTests
{
    [TestClass]
    public class UnitTestDriverAndConstructor
    {
        PopulateDriversAndTeams populateDriversAndTeams;
        ApplicationDbContext context;
        DriverModel driverModel = new DriverModel
        {
            Code = "Ver",
            Constructor = 12,
            DriverID = "max_verstappen",
            ID = 1,
            RaceAmount=0
        };
        ConstructorModel constructorModel = new ConstructorModel
        {
            ID = 1,
            ConstructorID = "red_bull",
            Name =  "RedBull",
            Nationality = "Austrian"
            
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
            populateDriversAndTeams = new PopulateDriversAndTeams(context);
        }

        [TestMethod]
        public async Task TestInsertDriver()
        {
            await populateDriversAndTeams.InsertDriver(driverModel);
            DriverModel driverCheck = await context.Driver.Where(x => x.ID == driverModel.ID).FirstOrDefaultAsync();
            Assert.IsTrue(driverCheck.ID >0);
        }        
        [TestMethod]
        public async Task TestInsertConstructors()
        {
            List<ConstructorModel> constructors = new List<ConstructorModel> { constructorModel };
            await populateDriversAndTeams.InsertConstrutors(constructors);
            ConstructorModel constructorCheck = await context.Constructor.Where(x => x.ID == constructorModel.ID).FirstOrDefaultAsync();
            Assert.IsTrue(constructorCheck.ID >0);
        }        
        [TestMethod]
        public async Task TestGetConstructorsIDByName()
        {
            List<ConstructorModel> constructors = new List<ConstructorModel> { constructorModel };
            await populateDriversAndTeams.InsertConstrutors(constructors);
            int constructorsID = await populateDriversAndTeams.GetConstructorID(constructorModel.ConstructorID);
            Assert.IsTrue(constructorModel.ID == constructorsID);
        }
    }
}
