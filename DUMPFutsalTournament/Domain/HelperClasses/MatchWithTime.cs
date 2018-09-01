using DUMPFutsalTournament.Data.Entities;

namespace DUMPFutsalTournament.Domain.HelperClasses
{
    public class MatchWithTime : Match
    {
        public MatchWithTime(Match match, int minute)
        {
            AwayGoals = match.AwayGoals ?? 0;
            AwayTeam = match.AwayTeam;
            HomeGoals = match.HomeGoals ?? 0;
            HomeTeam = match.HomeTeam;
            IsActive = match.IsActive;
            MatchEvents = match.MatchEvents;
            TimeOfMatch = match.TimeOfMatch;
            MatchType = match.MatchType;
            MatchId = match.MatchId;
            Minute = minute;
        }
        public int Minute { get; set; }
    }
}
