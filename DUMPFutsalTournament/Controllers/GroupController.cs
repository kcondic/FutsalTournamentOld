using DUMPFutsalTournament.Data.Entities;
using DUMPFutsalTournament.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DUMPFutsalTournament.Controllers
{
    [Route("api/groups")]
    public class GroupController : Controller
    {
        public GroupController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }
        private readonly IGroupRepository _groupRepository;

        [HttpGet]
        public IActionResult GetAllGroups()
        {
            return Ok(_groupRepository.GetAllGroups());
        }

        [Authorize]
        [HttpGet("{groupId}")]
        public IActionResult GetGroup(int groupId)
        {
            return Ok(_groupRepository.GetSpecificGroup(groupId));
        }

        [Authorize]
        [HttpPost("add")]
        public IActionResult AddGroup([FromBody]Group group)
        {
            _groupRepository.AddGroup(group);
            return Ok(null);
        }

        [Authorize]
        [HttpPost("edit")]
        public IActionResult EditGroup([FromBody]Group editedGroup)
        {
            _groupRepository.EditGroup(editedGroup);
            return Ok(null);
        }

        [Authorize]
        [HttpDelete("delete/{groupId}")]
        public IActionResult DeleteGroup(int groupId)
        {
            _groupRepository.DeleteGroup(groupId);
            return Ok(null);
        }

        [HttpGet]
        [HttpGet("standings")]
        public IActionResult GetAllGroupStandings()
        {
            return Ok(_groupRepository.GetCalculatedGroupStandings());
        }
    }
}
