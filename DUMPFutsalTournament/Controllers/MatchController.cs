using DUMPFutsalTournament.Data.Entities;
using DUMPFutsalTournament.Domain.HelperClasses;
using DUMPFutsalTournament.Domain.Implementations;
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

        [HttpGet("elimination")]
        public IActionResult GetEliminationMatches()
        {
            return Ok(_matchRepository.GetEliminationMatches());
        }

        [HttpGet("active")]
        public IActionResult GetActiveMatch()
        {
            var activeMatch = _matchRepository.GetActiveMatch();
            if (activeMatch == null)
                return Ok(null);
            var extendedMatch = new MatchWithTime(_matchRepository.GetActiveMatch(), LiveMatchService.CurrentActiveMatchMinute, LiveMatchService.CurrentActiveMatchSecond);
            return Ok(extendedMatch);
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
            LiveMatchService.CurrentActiveMatchMinute = 0;
            LiveMatchService.CurrentActiveMatchSecond = 0;
            _matchRepository.SetActiveMatch(matchId);
            return Ok(null);
        }

        [Authorize]
        [HttpPost("deactivate")]
        public IActionResult DeactivateMatch()
        {
            LiveMatchService.CurrentActiveMatchMinute = 0;
            LiveMatchService.CurrentActiveMatchSecond = 0;

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
