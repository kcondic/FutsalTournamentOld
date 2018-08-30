using System.Collections.Generic;
using DUMPFutsalTournament.Data.Entities;
using DUMPFutsalTournament.Domain.HelperClasses;
using DUMPFutsalTournament.Domain.Implementations;

namespace DUMPFutsalTournament.Domain.Interfaces
{
    public interface IMatchRepository
    {
        List<Match> GetAllMatches();
        Match GetActiveMatch();
        Match GetSpecificMatch(int matchId);
        List<Match> GetMatchesForTeam(int teamId);
        void AddMatch(Match match);
        void SetActiveMatch(int matchId);
        void DeactivateMatch();
        void EditMatch(Match editedMatch);
        void DeleteMatch(int matchId);
        void AddMatchEvent(MatchEvent matchEvent);
        void DeleteMatchEvent(int matchEventId);
        void UpdateMatchGoals(Match updatedMatch);
        List<StagingMatch> GetMatchesForBrackets();
    }
}
