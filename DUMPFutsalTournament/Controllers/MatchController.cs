using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DUMPFutsalTournament.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DUMPFutsalTournament.Controllers
{
    [Route("api/matches")]
    public class MatchController : Controller
    {
        [HttpGet]
        public List<Match> GetAllMatches()
        {
            return null;
        }

        [HttpGet("active")]
        public Match GetActiveMatch()
        {
            return null;
        }

        [HttpGet("{id}")]
        public Match GetSpecificMatch(int id)
        {
            return null;
        }
    }
}
