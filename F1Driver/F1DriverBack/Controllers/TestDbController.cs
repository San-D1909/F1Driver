using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace F1Driver_Back.Controllers
{
    [Route("[controller]/[action]")]
    public class TestDbController : Controller
    {
        private readonly IPopulateDatabase DriverClass;
        public TestDbController(IPopulateDatabase driver)
        {
            DriverClass = driver;
        }
/*        private readonly IConstructorClass ConstrutorClass;
        public TestDbController(IConstructorClass Construtor)
        {
            ConstrutorClass = Construtor;
        }*/
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("test");
        }
        [HttpPost]
        public async Task<IActionResult> InsertDriver(ModelLayer.DriverModel driverModel)
        {
            await DriverClass.Populate();
            return Ok();
        }
/*        [HttpPost]
        public async Task<IActionResult> InsertConstructor(ModelLayer.ConstructorModel constructorModel)
        {
            await ConstrutorClass.InsertConstrutor(constructorModel);
            return Ok();
        }*/
    }
}
