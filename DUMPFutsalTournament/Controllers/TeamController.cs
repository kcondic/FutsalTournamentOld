using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DUMPFutsalTournament.Data.Entities;
using DUMPFutsalTournament.Domain.Interfaces;
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

        [HttpPost("add")]
        public IActionResult AddTeam(Team team)
        {
            _teamRepository.AddTeam(team);
            return Ok(null);
        }

        [HttpPost("edit")]
        public IActionResult EditTeam(Team editedTeam)
        {
            _teamRepository.EditTeam(editedTeam);
            return Ok(null);
        }

        [HttpDelete("delete")]
        public IActionResult DeleteTeam(int teamId)
        {
            _teamRepository.DeleteTeam(teamId);
            return Ok(null);
        }

        [HttpGet("random")]
        public IActionResult GetRandomUngroupedTeams(int numberOfTeams)
        {
            return Ok(_teamRepository.GetRandomUngroupedTeams(numberOfTeams));
        }
    }
}
