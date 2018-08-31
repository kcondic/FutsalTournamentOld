using System.Collections.Generic;
using System.Linq;
using DUMPFutsalTournament.Data.Entities;

namespace DUMPFutsalTournament.Domain.HelperClasses
{
    public class MatchWithTime : Match
    {
        public MatchWithTime(Match match, int minute, int second, bool hasEvents)
        {
            AwayGoals = match.AwayGoals ?? 0;
            AwayTeam = match.AwayTeam;
            HomeGoals = match.HomeGoals ?? 0;
            HomeTeam = match.HomeTeam;
            IsActive = match.IsActive;
            if (hasEvents)
                MatchEvents = match.MatchEvents;
            else
            {
                MatchEvents = match.MatchEvents.Select(x =>
                {
                    x.Match = null;
                    x.Player.MatchEvents = new List<MatchEvent>();
                    x.Player.DateOfBirth = null;
                    x.Player.Team = null;
                    return x;
                }).ToList();
                HomeTeam.Players = new List<Player>();
                AwayTeam.Players = new List<Player>();
            }
            TimeOfMatch = match.TimeOfMatch;
            MatchType = match.MatchType;
            MatchId = match.MatchId;
            Minute = minute;
            Second = second;
        }
        public int Minute { get; set; }
        public int Second { get; set; }
    }
}
