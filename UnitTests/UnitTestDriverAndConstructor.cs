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
using DataLayer.Interfaces;
using ModelLayer;

namespace UnitTests
{
    [TestClass]
    public class UnitTestDriverAndConstructor
    {
        PopulateDriversAndTeams populateDriversAndTeams;
        GetStandings standings;
        ApplicationDbContext context;
        DriverModel driverModel = new DriverModel
        {
            Code = "Ver",
            Constructor = 12,
            DriverID = "max_verstappen",
            ID = 1,
            RaceAmount=0,
            Url = "http://en.wikipedia.org/wiki/Max_Verstappen",
            ImageUrl ="https://upload.wikimedia.org/wikipedia/commons/7/75/Max_Verstappen_2017_Malaysia_3.jpg"
        };
        ConstructorModel constructorModel = new ConstructorModel
        {
            ID = 1,
            ConstructorID = "red_bull",
            Name =  "RedBull",
            Nationality = "Austrian"

        };
        RaceResultModel raceResult = new RaceResultModel
        {
            ID = 1,
            Points = "21",
            Position = "1",
            Laps = "50",
            Race ="100",
            DriverID = "max_verstappen",
            ConstructorID = "red_bull"
        };
        [TestInitialize]
        public void TestInit()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            context = new ApplicationDbContext(options);
            populateDriversAndTeams = new PopulateDriversAndTeams(context);
            standings = new GetStandings(context);
        }

        [TestMethod]
        public async Task TestInsertDriver()
        {
            //Arrange
            await populateDriversAndTeams.InsertDriver(driverModel);
            //Act
            DriverModel driverCheck = await context.Driver.Where(x => x.ID == driverModel.ID).FirstOrDefaultAsync();
            //Assert
            Assert.IsTrue(driverCheck.ID >0);
        }
        [TestMethod]
        public async Task TestInsertConstructors()
        {
            //Arrange
            List<ConstructorModel> constructors = new List<ConstructorModel> { constructorModel };
            //Act
            await populateDriversAndTeams.InsertConstrutors(constructors);
            ConstructorModel constructorCheck = await context.Constructor.Where(x => x.ID == constructorModel.ID).FirstOrDefaultAsync();
            //Assert
            Assert.IsTrue(constructorCheck.ID >0);
        }
        [TestMethod]
        public async Task TestGetDriverStandingsINCLUDINGDriverImages()
        {    
            //Arrange
            await populateDriversAndTeams.InsertDriver(driverModel);
            List<ConstructorModel> constructors = new List<ConstructorModel>{constructorModel};
            await populateDriversAndTeams.InsertConstrutors(constructors);
            await context.RaceResult.AddAsync(raceResult);
            await context.SaveChangesAsync();
            //Act
            var results = await standings.GetCurrentStandings();
            //Assert
            Assert.IsTrue(results.Count()>0);
        }
        [TestMethod]
        public async Task TestGetConstructorsIDByName()
        {
            //Arrange
            List<ConstructorModel> constructors = new List<ConstructorModel> { constructorModel };
            await populateDriversAndTeams.InsertConstrutors(constructors);
            //Act
            int constructorsID = await populateDriversAndTeams.GetConstructorID(constructorModel.ConstructorID);
            //Assert
            Assert.IsTrue(constructorModel.ID == constructorsID);
        }
    }
}
