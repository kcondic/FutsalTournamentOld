using System.Collections.Generic;
using DUMPFutsalTournament.Data.Entities;

namespace DUMPFutsalTournament.Domain.Interfaces
{
    public interface IMatchRepository
    {
        List<Match> GetAllMatches();
        Match GetActiveMatch();
        Match GetSpecificMatch(int matchId);
        List<Match> GetMatchesForTeam(int teamId);
        void AddMatch(Match match);
        void EditMatch(Match editedMatch);
        void DeleteMatch(int matchId);
        void AddMatchEvent(int matchId, MatchEvent matchEvent);
    }
}
