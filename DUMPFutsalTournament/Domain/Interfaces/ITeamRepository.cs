using System.Collections.Generic;
using DUMPFutsalTournament.Data.Entities;

namespace DUMPFutsalTournament.Domain.Interfaces
{
    public interface ITeamRepository
    {
        List<Team> GetAllTeams();
        Team GetSpecificTeam(int teamId);
        void AddTeam(Team team);
        void EditTeam(Team editedTeam);
        void DeleteTeam(int teamId);
        List<Team> GetRandomUngroupedTeams(int numberOfTeams);
    }
}
