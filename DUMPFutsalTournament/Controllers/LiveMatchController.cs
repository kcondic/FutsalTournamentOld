using DUMPFutsalTournament.Data.Entities;
using DUMPFutsalTournament.Domain.Implementations;
using DUMPFutsalTournament.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DUMPFutsalTournament.Controllers
{
    [Route("api/live-match")]
    public class LiveMatchController : Controller
    {
        [Authorize]
        [HttpGet("update-minute")]
        public IActionResult UpdateMinute()
        {
            if (LiveMatchService.CurrentActiveMatchMinute >= 30)
                return BadRequest();

            LiveMatchService.CurrentActiveMatchMinute++;
            return Ok(null);
        }

        [HttpGet("get-time")]
        public IActionResult GetMinute()
        {
            return Ok(LiveMatchService.CurrentActiveMatchMinute);
        }


        [Authorize]
        [HttpGet("set-time")]
        public IActionResult SetTime(int minutes, int seconds)
        {
            LiveMatchService.CurrentActiveMatchMinute = minutes;
            return Ok(null);
        }
    }
}
