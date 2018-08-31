using System;
using System.Collections.Generic;
using System.Linq;
using DUMPFutsalTournament.Data;
using DUMPFutsalTournament.Data.Entities;
using DUMPFutsalTournament.Data.Enums;
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
            if (match.HomeTeam == null || match.AwayTeam == null || match.TimeOfMatch == null || 
                match.HomeGoals != null || match.AwayGoals != null || match.HomeTeam.TeamId == match.AwayTeam.TeamId)
                return;
            _context.Teams.Attach(match.HomeTeam);
            _context.Teams.Attach(match.AwayTeam);
            match.TimeOfMatch = match.TimeOfMatch.ToLocalTime();
            _context.Matches.Add(match);
            _context.SaveChanges();
        }

        public void SetActiveMatch(int matchId)
        {
            var currentlyActiveMatch = _context.Matches.SingleOrDefault(match => match.IsActive);
            var matchToActivate = _context.Matches.Find(matchId);

            if (matchToActivate == null || matchToActivate.HomeGoals != null || 
                matchToActivate.AwayGoals != null || matchToActivate.TimeOfMatch < DateTime.Now - TimeSpan.FromMinutes(60))
                return;

            if (currentlyActiveMatch != null)
                currentlyActiveMatch.IsActive = false;

            matchToActivate.IsActive = true;
            matchToActivate.HomeGoals = 0;
            matchToActivate.AwayGoals = 0;
            _context.SaveChanges();
        }

        public void DeactivateMatch()
        {
            var matchToDeactivate = _context.Matches.SingleOrDefault(match => match.IsActive);

            if (matchToDeactivate == null)
                return;

            matchToDeactivate.IsActive = false;
            _context.SaveChanges();
        }

        public void EditMatch(Match editedMatch)
        {
            var matchToEdit = _context.Matches.Find(editedMatch.MatchId);
            if (matchToEdit == null)
                return;
            matchToEdit.TimeOfMatch = editedMatch.TimeOfMatch.ToLocalTime();
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

        public void AddMatchEvent(MatchEvent matchEvent)
        {
            if(matchEvent.Player != null)
                _context.Players.Attach(matchEvent.Player);
            _context.Matches.Attach(matchEvent.Match);
            _context.MatchEvents.Add(matchEvent);
            _context.SaveChanges();
        }

        public void DeleteMatchEvent(int matchEventId)
        {
            var matchEventToDelete = _context.MatchEvents.Find(matchEventId);
            if (matchEventToDelete == null)
                return;
            _context.MatchEvents.Remove(matchEventToDelete);
            _context.SaveChanges();
        }

        public void UpdateMatchGoals(Match updatedMatch)
        {
            var matchToUpdate = _context.Matches.Find(updatedMatch.MatchId);

            if (matchToUpdate == null)
                return;

            matchToUpdate.HomeGoals = updatedMatch.HomeGoals;
            matchToUpdate.AwayGoals = updatedMatch.AwayGoals;
            _context.SaveChanges();
        }

        public List<Match> GetEliminationMatches()
        {
            return _context.Matches
                .Include(match => match.AwayTeam)
                .Include(match => match.HomeTeam)
                .Where(match => match.MatchType != MatchType.Group)
                .ToList();
        }
    }
}
