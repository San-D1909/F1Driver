using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BettingServiceBack;
using BettingServiceDataLayer;
using BettingServiceDataLayer.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore.InMemory;
using DataLayer.Interfaces;
using ModelLayer;

namespace UnitTests
{
    [TestClass]
    public class UnitTestBetting
    {
        BettingClass bettingClass;
        ApplicationDbContext context;
        public int category = 1;
        UserModel DatabaseUser = new UserModel
        {
            ID = 1,
            Password ="a73fa61dfab33f2c1086852f2df1c092",
            UserName = "tester",
            Email = "t@t"
        };
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
        RaceModel raceModel = new RaceModel
        {
            ID = 17,
            CircuitId="americas",
            Country = "United_States",
            RaceName="United States Grand Prix",
            Season = DateTime.Now.Year,
            Url = "http://en.wikipedia.org/wiki/2021_United_States_Grand_Prix",
            Date = DateTime.Now.AddMonths(1),//Add a month so the upcomming race method knows that it should be included
        };
        [TestInitialize]
        public void TestInit()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            context = new ApplicationDbContext(options);
            bettingClass = new BettingClass(context);
        }
        [TestMethod]
        public async Task TestInsertABet()
        {
            //Arrange
            await context.Driver.AddAsync(driverModel);
            await context.Race.AddAsync(raceModel);
            await context.User.AddAsync(DatabaseUser);
            await context.SaveChangesAsync();
            //Act
            bool result = await bettingClass.SetBet(driverModel.ID, category, DatabaseUser.ID, raceModel.ID);
            BetModel model = await context.Bet.Where(b => b.ID !=0).FirstOrDefaultAsync();
            //Assert
            Assert.IsTrue(model.ID >0);
        }
        [TestMethod]
        public async Task TestGetABet()
        {
            //Arrange
            await context.Driver.AddAsync(driverModel);
            await context.Race.AddAsync(raceModel);
            await context.User.AddAsync(DatabaseUser);
            await context.SaveChangesAsync();
            bool first = await bettingClass.SetBet(driverModel.ID, 1, DatabaseUser.ID, raceModel.ID);
            bool second = await bettingClass.SetBet(driverModel.ID, 2, DatabaseUser.ID, raceModel.ID);
            bool third = await bettingClass.SetBet(driverModel.ID, 3, DatabaseUser.ID, raceModel.ID);
            bool fastest = await bettingClass.SetBet(driverModel.ID, 4, DatabaseUser.ID, raceModel.ID);
            //Act
            if (first==true&&second==true&&third==true&&fastest==true)
            {
                ModelLayer.DTO.BetDTO Bet = await bettingClass.GetBet(DatabaseUser.ID, raceModel.ID);
                //Assert
                Assert.IsTrue(Bet.ID!=0);
            }
            else
            {
                //Assert
                Assert.Fail();
            }
        }
    }
}
