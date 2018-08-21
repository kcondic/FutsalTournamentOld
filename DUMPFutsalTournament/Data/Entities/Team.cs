using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUMPFutsalTournament.Data.Entities
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public Group Group { get; set; }
        public virtual ICollection<Match> HomeMatches { get; set; }
        public virtual ICollection<Match> AwayMatches { get; set; }
    }
}
