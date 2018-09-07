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
            var matches = _context.Matches
                .Include(match => match.HomeTeam)
                .Include(match => match.AwayTeam)
                .OrderBy(match => match.TimeOfMatch)
                .ToList();
            foreach (var match in matches)
            {
                match.HomeTeam.HomeMatches = null;
                match.HomeTeam.AwayMatches = null;
                match.HomeTeam.Players = null;

                match.AwayTeam.HomeMatches = null;
                match.AwayTeam.AwayMatches = null;
                match.AwayTeam.Players = null;
            }

            return matches;
        }

        public Match GetActiveMatch()
        {
            var activeMatch = _context.Matches
                .Include(match => match.HomeTeam)
                .ThenInclude(homeTeam => homeTeam.Players)
                .Include(match => match.AwayTeam)
                .ThenInclude(awayTeam => awayTeam.Players)
                .Include(match => match.MatchEvents)
                .SingleOrDefault(match => match.IsActive);

            if (activeMatch == null)
                return null;

            foreach (var ev in activeMatch.MatchEvents)
            {
                ev.Match = null;
                if (ev.Player != null)
                {
                    ev.Player.Team = null;
                    ev.Player.MatchEvents = null;
                }
            }

            return activeMatch;
        }

        public Match GetSpecificMatch(int matchId)
        {
            var specificMatch = _context.Matches
                .Include(match => match.HomeTeam)
                .Include(match => match.AwayTeam)
                .Include(match => match.MatchEvents)
                .ThenInclude(ev => ev.Player)
                .ThenInclude(player => player.Team)
                .SingleOrDefault(match => match.MatchId == matchId);

            if (specificMatch == null)
                return null;

            foreach (var ev in specificMatch.MatchEvents)
            {
                ev.Match = null;
                if (ev.Player != null)
                {
                    ev.Player.Team = null;
                    ev.Player.MatchEvents = null;
                }
            }

            specificMatch.HomeTeam.HomeMatches = null;
            specificMatch.HomeTeam.AwayMatches = null;
            specificMatch.HomeTeam.Players = null;
            specificMatch.AwayTeam.HomeMatches = null;
            specificMatch.AwayTeam.AwayMatches = null;
            specificMatch.AwayTeam.Players = null;

            return specificMatch;
        }

        public List<Match> GetMatchesForTeam(int teamId)
        {
            var teamMatches = _context.Matches
                .Include(match => match.HomeTeam)
                .Include(match => match.AwayTeam)
                .Include(match => match.MatchEvents)
                .ThenInclude(ev => ev.Player)
                .ThenInclude(player => player.Team)
                .Where(match => 
                    match.HomeTeam.TeamId == teamId || match.AwayTeam.TeamId == teamId)
                .ToList();

            foreach (var match in teamMatches)
            {
                match.HomeTeam.HomeMatches = null;
                match.HomeTeam.AwayMatches = null;
                match.HomeTeam.Players = null;
                match.AwayTeam.HomeMatches = null;
                match.AwayTeam.AwayMatches = null;
                match.AwayTeam.Players = null;

                foreach (var ev in match.MatchEvents)
                {
                    ev.Match = null;
                    if (ev.Player != null)
                    {
                        ev.Player.Team = null;
                        ev.Player.MatchEvents = null;
                    }
                }
            }

            return teamMatches;
        }

        public void AddMatch(Match match)
        {
            if (match.HomeTeam == null || match.AwayTeam == null || match.TimeOfMatch == null || 
                match.HomeGoals != null || match.AwayGoals != null || match.HomeTeam.TeamId == match.AwayTeam.TeamId)
                return;
            _context.Teams.Attach(match.HomeTeam);
            _context.Teams.Attach(match.AwayTeam);
            var croatianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            match.TimeOfMatch = TimeZoneInfo.ConvertTimeFromUtc(match.TimeOfMatch, croatianTimeZone);
            _context.Matches.Add(match);
            _context.SaveChanges();
        }

        public void SetActiveMatch(int matchId)
        {
            var currentlyActiveMatch = _context.Matches.SingleOrDefault(match => match.IsActive);
            var matchToActivate = _context.Matches.Find(matchId);

            if (matchToActivate == null || matchToActivate.HomeGoals != null || 
                matchToActivate.AwayGoals != null)
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
            var croatianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            matchToEdit.TimeOfMatch = TimeZoneInfo.ConvertTimeFromUtc(editedMatch.TimeOfMatch, croatianTimeZone);
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
            var newMatchEvent = new MatchEvent
            {
                EventMinute = matchEvent.EventMinute,
                IsForHomeTeam = matchEvent.IsForHomeTeam,
                MatchId = matchEvent.Match.MatchId,
                PlayerId = matchEvent.Player?.PlayerId,
                EventType = matchEvent.EventType
            };
            _context.MatchEvents.Add(newMatchEvent);
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
    }
}
