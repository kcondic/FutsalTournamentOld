using DUMPFutsalTournament.Data.Entities;

namespace DUMPFutsalTournament.Domain.HelperClasses
{
    public class GroupStanding
    {
        public Team Team { get; set; }
        public int NumberOfGames { get; set; }
        public int Points { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsConceded { get; set; }
    }
}
