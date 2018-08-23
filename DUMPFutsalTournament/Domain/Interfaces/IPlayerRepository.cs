using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DUMPFutsalTournament.Data.Entities;

namespace DUMPFutsalTournament.Domain.Interfaces
{
    public interface IPlayerRepository
    {
        List<Player> GetAllPlayers();
        Player GetSpecificPlayer(int playerId);
        void AddPlayer(Player player);
        void EditPlayer(Player editedPlayer);
        void DeletePlayer(int playerId);
    }
}
