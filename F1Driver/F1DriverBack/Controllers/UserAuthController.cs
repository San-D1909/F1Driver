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
        private readonly IRegister RegisterClass;
        private readonly IConvertTokens _convertTokens;
        public UserAuthController(ILogin _loginClass, IRegister _registerClass,IConvertTokens convertTokens)
        {
            LoginClass = _loginClass;
            RegisterClass = _registerClass;
            _convertTokens = convertTokens;
        }
        [HttpPost("LogInFunction")]
        public async Task<IActionResult> LogIn([FromBody] UserModel credentials)
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
        [HttpPost("RegisterFunction")]
        public async Task<IActionResult> Register([FromBody] UserModel credentials)
        {
            try
            {
                var token = await RegisterClass.RegisterMethod(credentials);
                return Ok(TokenClass.WriteToken(token));
            }
            catch
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }

        }
        [HttpGet("GetUserByToken")]
        public async Task<ActionResult> GetUserByToken([FromQuery] string jtoken)
        {//Gets the user  by the token from the front end.
            return Ok(await _convertTokens.TokenConverterMethod(jtoken));
        }
    }
}
