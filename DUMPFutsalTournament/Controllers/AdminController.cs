using DUMPFutsalTournament.Data.Entities;
using DUMPFutsalTournament.Domain.HelperClasses.Auth;
using DUMPFutsalTournament.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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

            return Ok(JwtHelper.GetJwtToken(user));
        }

        [Authorize]
        [HttpPost("api/add-user")]
        public IActionResult AddUser([FromBody]User userToAdd)
        {
            var wasUserAdded = _loginRepository.AddUser(userToAdd);

            if (!wasUserAdded)
                return Forbid();

            return Ok(null);
        }

        [Authorize]
        [HttpPost("api/change-password")]
        public IActionResult ChangePassword([FromBody]JObject dataToChange)
        {
            var oldPassword = dataToChange["oldPassword"].ToObject<string>();
            var newPassword = dataToChange["newPassword"].ToObject<string>();
            var userId = dataToChange["userId"].ToObject<int>();

            var user = _loginRepository.GetById(userId);

            if (user == null)
                return NotFound();

            if(HashHelper.ValidatePassword(oldPassword, user.Password))
                _loginRepository.ChangePassword(user, newPassword);
            else
                return Forbid();

            return Ok(null);
        }
    }
}
