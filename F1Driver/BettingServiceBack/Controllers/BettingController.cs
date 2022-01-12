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
        public async Task<IActionResult> SetBet([FromQuery] int driver,[FromQuery] int category,[FromQuery] int userID, [FromQuery] int race)
        {
            return Ok(await Bet.SetBet(driver,category,userID,race));
        }        
        [HttpPost("GetBet")]
        public async Task<IActionResult> GetBet([FromQuery] int race,[FromQuery] int userID)
        {
            if(race==0&&userID ==0)
            {
                return Ok();
            }
            return Ok(await Bet.GetBet(userID, race));
        }
    }
}
