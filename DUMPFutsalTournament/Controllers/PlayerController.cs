using DUMPFutsalTournament.Data.Entities;
using DUMPFutsalTournament.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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
            _playerRepository.AddPlayer(player);
            return Ok(null);
        }

        [Authorize]
        [HttpPost("edit")]
        public IActionResult EditPlayer([FromBody]Player editedPlayer)
        {
            _playerRepository.EditPlayer(editedPlayer);
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