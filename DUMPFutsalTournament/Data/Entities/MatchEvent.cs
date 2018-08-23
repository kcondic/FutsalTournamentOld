﻿using DUMPFutsalTournament.Data.Enums;

namespace DUMPFutsalTournament.Data.Entities
{
    public class MatchEvent
    {
        public int MatchEventId { get; set; }
        public int EventMinute { get; set; }
        public Match Match { get; set; }
        public Player Player { get; set; }
        public MatchEventType EventType { get; set; }
    }
}
