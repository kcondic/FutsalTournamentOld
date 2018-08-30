

using System;
using DUMPFutsalTournament.Data.Enums;

namespace DUMPFutsalTournament.Domain.HelperClasses
{
    public class StagingMatch
    {
        public int? MatchId { get; set; }
        public string HomeName { get; set; }
        public string AwayName { get; set; }

        public int? HomeGoals { get; set; }
        public int? AwayGoals { get; set; }

        public MatchType MatchType { get; set; }

        public DateTime TimeOfMatch { get; set; }
    }
}
