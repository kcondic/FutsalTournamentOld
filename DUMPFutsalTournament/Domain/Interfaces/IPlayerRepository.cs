using System.Collections.Generic;
using DUMPFutsalTournament.Data.Entities;

namespace DUMPFutsalTournament.Domain.Interfaces
{
    public interface IPlayerRepository
    {
        List<Player> GetAllPlayers();
        void AddPlayer(Player player);
        void EditPlayer(Player editedPlayer);
        void DeletePlayer(int playerId);
    }
}
