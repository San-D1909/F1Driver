using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataLayer;
using Microsoft.EntityFrameworkCore.InMemory;
using DataLayer.Classes;
using DataLayer.Classes.RegisterAndLogin;
using ModelLayer;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DataLayer.Helpers;

namespace UnitTests
{
    [TestClass]
    public class UnitTestLoginAndRegister
    {
        Login loginClass;
        ApplicationDbContext context;
        IConfiguration configuration;
        UserModel DatabaseUser = new UserModel
        {
            ID = 1,
            Password ="a73fa61dfab33f2c1086852f2df1c092",
            UserName = "tester",
            Email = "t@t"
        };

        [TestInitialize]
        public void TestInit()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            context = new ApplicationDbContext(options);
            configuration = builder.Build();
            loginClass = new Login(context, configuration);
        }

        [TestMethod]
        public async Task TestLogin()
        {
            //Arrange
            await context.User.AddAsync(DatabaseUser);
            await context.SaveChangesAsync();
            //Act
            UserModel credentials = new UserModel { Email = "t@t", Password ="test" };
            string token = TokenClass.WriteToken(await loginClass.Authenticate(credentials));
            //Assert
            Assert.IsTrue(token.Length>30);
        }
    }
}
