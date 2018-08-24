using DUMPFutsalTournament.Data.Entities;
using DUMPFutsalTournament.Domain.Auth;
using DUMPFutsalTournament.Domain.HelperClasses;
using DUMPFutsalTournament.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DUMPFutsalTournament.Controllers
{
    public class AdminController : Controller
    {
        public AdminController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }
        private readonly ILoginRepository _loginRepository;

        [HttpPost("api/login")]
        public IActionResult Login([FromBody]User userCredentials)
        {
            var user = _loginRepository.GetByUsername(userCredentials.Username);
            if (user == null) return NotFound();

            var areCredentialsValid = HashHelper.ValidatePassword(userCredentials.Password, user.Password);
            if (!areCredentialsValid) return Unauthorized();

            return Ok(JwtHelper.GetJwtToken(userCredentials));
        }

        [Authorize]
        [HttpPost("api/add-user")]
        public IActionResult AddUser([FromBody]User userToAdd)
        {
            userToAdd.Password = HashHelper.Hash(userToAdd.Password);
            var wasUserAdded = _loginRepository.AddUser(userToAdd);

            if (!wasUserAdded)
                return Forbid();

            return Ok();
        }
    }
}
