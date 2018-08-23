using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DUMPFutsalTournament.Data.Entities;

namespace DUMPFutsalTournament.Domain.Interfaces
{
    public interface IMatchRepository
    {
        List<Match> GetAllMatches();
        Match GetActiveMatch();
        Match GetSpecificMatch(int matchId);
        void AddMatch(Match match);
        void EditMatch(Match editedMatch);
        void DeleteMatch(int matchId);
        void AddMatchEvent(int matchId, MatchEvent matchEvent);
    }
}
