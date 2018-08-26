using System.Collections.Generic;
using System.Linq;
using DUMPFutsalTournament.Data;
using DUMPFutsalTournament.Data.Entities;
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

        public void AddPlayer(Player player)
        {
            if (player.FirstName == null || player.LastName == null)
                return;
            if (player.Team != null)
                _context.Teams.Attach(player.Team);
            _context.Players.Add(player);
            _context.SaveChanges();
        }

        public void EditPlayer(Player editedPlayer)
        {
            var playerToEdit = _context.Players.Find(editedPlayer.PlayerId);
            if (playerToEdit == null || editedPlayer.FirstName == null || editedPlayer.LastName == null)
                return;
            _context.Teams.Attach(editedPlayer.Team);
            playerToEdit.FirstName = editedPlayer.FirstName;
            playerToEdit.LastName = editedPlayer.LastName;
            playerToEdit.DateOfBirth = editedPlayer.DateOfBirth;
            playerToEdit.Team = editedPlayer.Team;
            _context.SaveChanges();
        }

        public void DeletePlayer(int playerId)
        {
            var playerToDelete = _context.Players.Find(playerId);
            if (playerToDelete == null)
                return;
            _context.Remove(playerToDelete);
            _context.SaveChanges();
        }
    }
}
