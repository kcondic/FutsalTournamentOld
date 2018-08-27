using DUMPFutsalTournament.Data.Entities;
using DUMPFutsalTournament.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DUMPFutsalTournament.Controllers
{
    [Route("api/players")]
    public class PlayerController : Controller
    {
        public PlayerController(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }
        private readonly IPlayerRepository _playerRepository;

        [Authorize]
        [HttpGet]
        public IActionResult GetAllPlayers()
        {
            return Ok(_playerRepository.GetAllPlayers());
        }

        [Authorize]
        [HttpGet("{playerId}")]
        public IActionResult GetSpecificPlayer(int playerId)
        {
            return Ok(_playerRepository.GetSpecificPlayer(playerId));
        }

        [Authorize]
        [HttpPost("add")]
        public IActionResult AddPlayer([FromBody]Player player)
        {
            var wasAdded = _playerRepository.AddPlayer(player);

            if (!wasAdded)
                return Forbid();

            return Ok(null);
        }

        [Authorize]
        [HttpPost("edit")]
        public IActionResult EditPlayer([FromBody]Player editedPlayer)
        {
            var wasUpdated = _playerRepository.EditPlayer(editedPlayer);

            if (!wasUpdated)
                return Forbid();

            return Ok(null);
        }

        [Authorize]
        [HttpDelete("delete/{playerId}")]
        public IActionResult DeletePlayer(int playerId)
        {
            _playerRepository.DeletePlayer(playerId);
            return Ok(null);
        }
    }
}