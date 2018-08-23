using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using DUMPFutsalTournament.Data.Enums;

namespace DUMPFutsalTournament.Data.Entities
{
    public class Match
    {
        public int MatchId { get; set; }
        public DateTime TimeOfMatch { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
        public MatchType MatchType { get; set; }
        public ICollection<MatchEvent> MatchEvents { get; set; }
        public bool IsActive { get; set; }
    }
}
