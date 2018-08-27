using System.Collections.Generic;
using System.Linq;
using DUMPFutsalTournament.Data;
using DUMPFutsalTournament.Data.Entities;
using DUMPFutsalTournament.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DUMPFutsalTournament.Domain.Implementations
{
    public class MatchRepository : IMatchRepository
    {
        public MatchRepository(FutsalContext context)
        {
            _context = context;
        }
        private readonly FutsalContext _context;

        public List<Match> GetAllMatches()
        {
            return _context.Matches
                .Include(match => match.HomeTeam)
                .Include(match => match.AwayTeam)
                .OrderBy(match => match.TimeOfMatch)
                .ToList();
        }

        public Match GetActiveMatch()
        {
            return _context.Matches
                .Include(match => match.HomeTeam)
                .ThenInclude(homeTeam => homeTeam.Players)
                .Include(match => match.AwayTeam)
                .ThenInclude(awayTeam => awayTeam.Players)
                .Include(match => match.MatchEvents)
                .ThenInclude(ev => ev.Player)
                .ThenInclude(player => player.Team)
                .SingleOrDefault(match => match.IsActive);
        }

        public Match GetSpecificMatch(int matchId)
        {
            return _context.Matches
                .Include(match => match.HomeTeam)
                .Include(match => match.AwayTeam)
                .Include(match => match.MatchEvents)
                .ThenInclude(ev => ev.Player)
                .ThenInclude(player => player.Team)
                .SingleOrDefault(match => match.MatchId == matchId);
        }

        public List<Match> GetMatchesForTeam(int teamId)
        {
            return _context.Matches
                .Include(match => match.HomeTeam)
                .Include(match => match.AwayTeam)
                .Include(match => match.MatchEvents)
                .ThenInclude(ev => ev.Player)
                .ThenInclude(player => player.Team)
                .Where(match => 
                    match.HomeTeam.TeamId == teamId || match.AwayTeam.TeamId == teamId)
                .ToList();
        }

        public void AddMatch(Match match)
        {
            _context.Teams.Attach(match.HomeTeam);
            _context.Teams.Attach(match.AwayTeam);
            _context.Matches.Add(match);
            _context.SaveChanges();
        }

        public void SetActiveMatch(int matchId)
        {
            var currentlyActiveMatch = _context.Matches.SingleOrDefault(match => match.IsActive);
            var matchToActivate = _context.Matches.Find(matchId);

            if (matchToActivate == null)
                return;

            if (currentlyActiveMatch != null)
                currentlyActiveMatch.IsActive = false;

            matchToActivate.IsActive = true;
            _context.SaveChanges();
        }

        public void EditMatch(Match editedMatch)
        {
            var matchToEdit = _context.Matches.Find(editedMatch.MatchId);
            if (matchToEdit == null)
                return;
            _context.Teams.Attach(matchToEdit.HomeTeam);
            _context.Teams.Attach(matchToEdit.AwayTeam);
            matchToEdit.TimeOfMatch = editedMatch.TimeOfMatch;
            matchToEdit.HomeGoals = editedMatch.HomeGoals;
            matchToEdit.AwayGoals = editedMatch.AwayGoals;
            _context.SaveChanges();
        }

        public void DeleteMatch(int matchId)
        {
            var matchToDelete = _context.Matches
                .Include(match => match.MatchEvents)
                .SingleOrDefault(match => match.MatchId == matchId);
            if (matchToDelete == null)
                return;
            _context.RemoveRange(matchToDelete.MatchEvents);
            _context.Remove(matchToDelete);
            _context.SaveChanges();
        }

        public void AddMatchEvent(int matchId, MatchEvent matchEvent)
        {
            var matchToAddTo = _context.Matches
                .Include(match => match.MatchEvents)
                .SingleOrDefault(match => match.MatchId == matchId);
            if (matchToAddTo == null)
                return;
            matchToAddTo.MatchEvents.Add(matchEvent);
            _context.SaveChanges();
        }
    }
}
