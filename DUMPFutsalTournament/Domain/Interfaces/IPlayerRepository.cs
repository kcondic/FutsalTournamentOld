using System.Collections.Generic;
using DUMPFutsalTournament.Data.Entities;
using DUMPFutsalTournament.Domain.HelperClasses;

namespace DUMPFutsalTournament.Domain.Interfaces
{
    public interface IPlayerRepository
    {
        List<Player> GetAllPlayers();
        Player GetSpecificPlayer(int playerId);
        bool AddPlayer(Player player);
        bool EditPlayer(Player editedPlayer);
        void DeletePlayer(int playerId);
        List<TopScorer> GetTopScorers();
    }
}
