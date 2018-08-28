using System;
using System.Collections.Generic;

namespace DUMPFutsalTournament.Data.Entities
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Team Team { get; set; }
        public ICollection<MatchEvent> MatchEvents { get; set; }
    }
}
