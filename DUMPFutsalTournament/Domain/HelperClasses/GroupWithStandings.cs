using System.Collections.Generic;
using DUMPFutsalTournament.Data.Entities;

namespace DUMPFutsalTournament.Domain.HelperClasses
{
    public class GroupWithStandings
    {
        public Group Group { get; set; }
        public List<GroupStanding> GroupStandings { get; set; }
    }
}
