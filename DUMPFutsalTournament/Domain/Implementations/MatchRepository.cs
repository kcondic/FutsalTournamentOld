using System;
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
                .ToList();
        }

        public Match GetActiveMatch()
        {
            return _context.Matches
                .Include(match => match.HomeTeam)
                .Include(match => match.AwayTeam)
                .Include(match => match.MatchEvents)
                .SingleOrDefault(match => DateTime.Now > match.TimeOfMatch - TimeSpan.FromMinutes(5));
        }

        public Match GetSpecificMatch(int matchId)
        {
            return _context.Matches
                .Include(match => match.HomeTeam)
                .Include(match => match.AwayTeam)
                .Include(match => match.MatchEvents)
                .SingleOrDefault(match => match.MatchId == matchId);
        }

        public void AddMatch(Match match)
        {
            _context.Teams.Attach(match.HomeTeam);
            _context.Teams.Attach(match.AwayTeam);
            _context.Matches.Add(match);
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
