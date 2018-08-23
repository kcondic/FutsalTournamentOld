using System.Collections.Generic;

namespace DUMPFutsalTournament.Data.Entities
{
    public class Group
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public ICollection<Team> Teams { get; set; }
    }
}
