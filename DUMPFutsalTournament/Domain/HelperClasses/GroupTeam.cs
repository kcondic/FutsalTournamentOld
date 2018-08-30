namespace DUMPFutsalTournament.Domain.HelperClasses
{
    public class GroupTeam
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }

        public int MatchesPlayed { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsTaken { get; set; }
        public int Points { get; set; }
    }
}
