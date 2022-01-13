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
using DataLayer.Interfaces;


namespace UnitTests
{
    [TestClass]
    public class UnitTestCircuit
    {
        PopulateCircuits populateCircuits;
        ApplicationDbContext context;
        CircuitModel circuitModel = new CircuitModel
        {
            ID = 17,
            CircuitID="americas",
            Locality = "Austin",
            Country = "United_States",
            CircuitName="United States Grand Prix",
        };

        [TestInitialize]
        public void TestInit()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            context = new ApplicationDbContext(options);
            populateCircuits = new PopulateCircuits(context);
        }

        [TestMethod]
        public async Task TestInsertCircuits()
        {
            //Arrange
            List<CircuitModel> circuits = new List<CircuitModel> {circuitModel };
            await populateCircuits.InsertCircuits(circuits);
            //Act
            CircuitModel CircuitCheck = await context.Circuit.Where(x => x.ID == circuitModel.ID).FirstOrDefaultAsync();
            //Assert
            Assert.IsTrue(CircuitCheck.ID >0);
        }
    }
}
