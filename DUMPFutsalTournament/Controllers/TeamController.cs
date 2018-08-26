using DUMPFutsalTournament.Data.Entities;
using DUMPFutsalTournament.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DUMPFutsalTournament.Controllers
{
    [Route("api/teams")]
    public class TeamController : Controller
    {
        public TeamController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }
        private readonly ITeamRepository _teamRepository;

        [HttpGet]
        public IActionResult GetAllTeams()
        {
            return Ok(_teamRepository.GetAllTeams());
        }

        [HttpGet("{teamId}")]
        public IActionResult GetSpecificTeam(int teamId)
        {
            return Ok(_teamRepository.GetSpecificTeam(teamId));
        }

        [Authorize]
        [HttpPost("add")]
        public IActionResult AddTeam([FromBody]Team team)
        {
            _teamRepository.AddTeam(team);
            return Ok(null);
        }

        [Authorize]
        [HttpPost("edit")]
        public IActionResult EditTeam([FromBody]Team editedTeam)
        {
            _teamRepository.EditTeam(editedTeam);
            return Ok(null);
        }

        [Authorize]
        [HttpDelete("delete")]
        public IActionResult DeleteTeam(int teamId)
        {
            _teamRepository.DeleteTeam(teamId);
            return Ok(null);
        }
    }
}
