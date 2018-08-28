using DUMPFutsalTournament.Data.Entities;
using DUMPFutsalTournament.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("team/{teamId}")]
        public IActionResult GetMatchesForTeam(int teamId)
        {
            return Ok(_matchRepository.GetMatchesForTeam(teamId));
        }

        [Authorize]
        [HttpPost("add")]
        public IActionResult AddMatch([FromBody]Match match)
        {
            _matchRepository.AddMatch(match);
            return Ok(null);
        }

        [Authorize]
        [HttpPost("activate")]
        public IActionResult SetActiveMatch([FromBody]int matchId)
        {
            _matchRepository.SetActiveMatch(matchId);
            return Ok(null);
        }

        [Authorize]
        [HttpPost("deactivate")]
        public IActionResult DeactivateMatch()
        {
            _matchRepository.DeactivateMatch();
            return Ok(null);
        }

        [Authorize]
        [HttpPost("edit")]
        public IActionResult EditMatch([FromBody]Match match)
        {
            _matchRepository.EditMatch(match);
            return Ok(null);
        }

        [Authorize]
        [HttpDelete("delete/{matchId}")]
        public IActionResult DeleteMatch(int matchId)
        {
            _matchRepository.DeleteMatch(matchId);
            return Ok(null);
        }

        [Authorize]
        [HttpPost("add-event")]
        public IActionResult AddMatchEvent([FromBody]MatchEvent matchEvent)
        {
            _matchRepository.AddMatchEvent(matchEvent);
            return Ok(null);
        }

        [Authorize]
        [HttpDelete("delete-event/{matchEventId}")]
        public IActionResult DeleteMatchEvent(int matchEventId)
        {
            _matchRepository.DeleteMatchEvent(matchEventId);
            return Ok(null);
        }

        [Authorize]
        [HttpPost("update-match-goals")]
        public IActionResult UpdateMatchGoals([FromBody]Match updatedMatch)
        {
            _matchRepository.UpdateMatchGoals(updatedMatch);
            return Ok(null);
        }
    }
}
