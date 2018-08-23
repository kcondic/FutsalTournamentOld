using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DUMPFutsalTournament.Data.Entities;
using DUMPFutsalTournament.Domain.Interfaces;
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

        [HttpGet("standings/{groupId}")]
        public IActionResult GetCalculatedGroupStandings(int groupId)
        {
            return Ok(_groupRepository.GetCalculatedGroupStandings(groupId));
        }

        [HttpPost("add")]
        public IActionResult AddGroup(Group group)
        {
            _groupRepository.AddGroup(group);
            return Ok(null);
        }

        [HttpPost("edit")]
        public IActionResult EditGroup(Group editedGroup)
        {
            _groupRepository.EditGroup(editedGroup);
            return Ok(null);
        }

        [HttpDelete("delete")]
        public IActionResult DeleteGroup(int groupId)
        {
            _groupRepository.DeleteGroup(groupId);
            return Ok(null);
        }
    }
}
