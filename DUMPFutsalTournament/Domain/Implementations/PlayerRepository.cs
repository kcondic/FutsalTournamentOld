using System.Collections.Generic;
using System.Linq;
using DUMPFutsalTournament.Data;
using DUMPFutsalTournament.Data.Entities;
using DUMPFutsalTournament.Data.Enums;
using DUMPFutsalTournament.Domain.HelperClasses;
using DUMPFutsalTournament.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DUMPFutsalTournament.Domain.Implementations
{
    public class PlayerRepository : IPlayerRepository
    {
        public PlayerRepository(FutsalContext context)
        {
            _context = context;
        }
        private readonly FutsalContext _context;

        public List<Player> GetAllPlayers()
        {
            return _context.Players
                .Include(player => player.Team)
                .ToList();
        }

        public Player GetSpecificPlayer(int playerId)
        {
            return _context.Players
                .Include(player => player.Team)
                .SingleOrDefault(player => player.PlayerId == playerId);
        }

        public bool AddPlayer(Player player)
        {
            if (player.LastName == null)
                return false;   
            if (player.Team != null && GetTeamPlayerCount(player.Team.TeamId) >= 12)
                return false;
            if (player.Team != null)
                _context.Teams.Attach(player.Team);
            _context.Players.Add(player);
            _context.SaveChanges();
            return true;
        }

        public bool EditPlayer(Player editedPlayer)
        {
            var playerToEdit = _context.Players.Find(editedPlayer.PlayerId);
            if (playerToEdit == null || editedPlayer.LastName == null)
                return false;
            if (editedPlayer.Team != null && GetTeamPlayerCount(editedPlayer.Team.TeamId) > 12)
                return false;
            if (editedPlayer.Team != null)
                _context.Teams.Attach(editedPlayer.Team);
            playerToEdit.FirstName = editedPlayer.FirstName;
            playerToEdit.LastName = editedPlayer.LastName;
            playerToEdit.DateOfBirth = editedPlayer.DateOfBirth;
            playerToEdit.Team = editedPlayer.Team;
            _context.SaveChanges();
            return true;
        }

        public void DeletePlayer(int playerId)
        {
            var playerToDelete = _context.Players.Find(playerId);
            if (playerToDelete == null)
                return;
            _context.Remove(playerToDelete);
            _context.SaveChanges();
        }

        private int GetTeamPlayerCount(int teamId)
        {
            var team = _context.Teams.AsNoTracking().Include(t => t.Players).Single(t => t.TeamId == teamId);     
            return team.Players.Count;
        }

        public List<TopScorer> GetTopScorers()
        {
            var topScorers = _context.Players
                .Include(player => player.Team)
                .Include(player => player.MatchEvents)
                .Where(player => player.Team != null)
                .Select(player => new TopScorer
                    {
                        Player = player,
                        Goals = player.MatchEvents.Count(ev =>
                            ev.EventType == MatchEventType.Goal || ev.EventType == MatchEventType.PenaltyGoal)
                    })
                .ToList()
                .OrderByDescending(pl => pl.Goals)
                .Take(10)
                .ToList();

            foreach (var scorer in topScorers)
            {
                scorer.Player.Team.Players = null;
                scorer.Player.Team.HomeMatches = null;
                scorer.Player.Team.AwayMatches = null;
                scorer.Player.Team.Group = null;
                foreach (var matchEvent in scorer.Player.MatchEvents)
                {
                    matchEvent.Match = null;
                    matchEvent.Player = null;
                }
            }

            return topScorers;
        }
    }
}
