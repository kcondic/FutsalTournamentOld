using DUMPFutsalTournament.Data.Entities;
using DUMPFutsalTournament.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DUMPFutsalTournament.Controllers
{
    [Route("api/matches")]
    public class MatchController : Controller
    {
        public MatchController(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }
        private readonly IMatchRepository _matchRepository;

        [HttpGet]
        public IActionResult GetAllMatches()
        {
            return Ok(_matchRepository.GetAllMatches());
        }

        [HttpGet("active")]
        public IActionResult GetActiveMatch()
        {
            return Ok(_matchRepository.GetActiveMatch());
        }

        [HttpGet("{matchId}")]
        public IActionResult GetSpecificMatch(int matchId)
        {
            return Ok(_matchRepository.GetSpecificMatch(matchId));
        }

        [HttpPost("add")]
        public IActionResult AddMatch(Match match)
        {
            _matchRepository.AddMatch(match);
            return Ok(null);
        }

        [HttpPost("edit")]
        public IActionResult EditMatch(Match match)
        {
            _matchRepository.EditMatch(match);
            return Ok(null);
        }

        [HttpDelete("delete")]
        public IActionResult DeleteMatch(int matchId)
        {
            _matchRepository.DeleteMatch(matchId);
            return Ok(null);
        }

        [HttpPost("add-event")]
        public IActionResult AddMatchEvent(int matchId, MatchEvent matchEvent)
        {
            _matchRepository.AddMatchEvent(matchId, matchEvent);
            return Ok(null);
        }
    }
}
