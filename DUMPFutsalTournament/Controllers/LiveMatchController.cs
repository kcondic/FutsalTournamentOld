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
        [HttpGet("update-second")]
        public IActionResult UpdateSecond()
        {
            if (LiveMatchService.CurrentActiveMatchMinute >= 30)
                return BadRequest();

            LiveMatchService.CurrentActiveMatchSecond++;
            if (LiveMatchService.CurrentActiveMatchSecond >= 60)
            {
                LiveMatchService.CurrentActiveMatchSecond = 0;
                LiveMatchService.CurrentActiveMatchMinute++;
            }

            return Ok();
        }

        [HttpGet("get-time")]
        public IActionResult GetMinute()
        {
            return Ok(new { minute = LiveMatchService.CurrentActiveMatchMinute, second = LiveMatchService.CurrentActiveMatchSecond});
        }
    }
}
