using System.Collections.Generic;

namespace DUMPFutsalTournament.Domain.HelperClasses
{
    public class ExtendedGroup
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }

        public List<GroupTeam> Teams { get; set; }
    }
}
