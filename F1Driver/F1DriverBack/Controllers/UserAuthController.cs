using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelLayer;
using DataLayer.Interfaces;
using DataLayer.Helpers;
using Microsoft.AspNetCore.Http;

namespace F1DriverBack.Controllers
{
    [Route("[controller]/[action]")]
    public class UserAuthController : Controller
    {
        private readonly ILogin LoginClass;
        public UserAuthController(ILogin _loginClass)
        {
            LoginClass = _loginClass;
        }
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("LogInFunction")]
        public async Task<IActionResult> LogInFunction([FromQuery] UserModel credentials)
        {
            try
            {
                var token = await LoginClass.Authenticate(credentials);
                return Ok(TokenClass.WriteToken(token));
            }
            catch
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }
    }
}
