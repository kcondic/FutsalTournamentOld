using System.Collections.Generic;
using DUMPFutsalTournament.Data.Entities;

namespace DUMPFutsalTournament.Domain.Interfaces
{
    public interface ITeamRepository
    {
        List<Team> GetAllTeams();
        List<Team> GetAllGrouplessTeams();
        Team GetSpecificTeam(int teamId);
        void AddTeam(Team team);
        void EditTeam(Team editedTeam);
        void DeleteTeam(int teamId);
    }
}
