using BettingServiceDataLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingServiceBack.Controllers
{
    [Route("[controller]/[action]")]
    public class BettingController : Controller
    {
        private readonly IBet Bet;
        public BettingController(IBet bet)
        {
            Bet = bet;
        }
        [HttpPost("SetBet")]
        public async Task<IActionResult> SetBet([FromQuery] string driver,[FromQuery] string category,[FromQuery] int userID, [FromQuery] int race)
        {
            return Ok(await Bet.SetBet(driver,category,userID,race));
        }
    }
}
