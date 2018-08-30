using System.Collections.Generic;
using System.Linq;
using DUMPFutsalTournament.Data;
using DUMPFutsalTournament.Data.Entities;
using DUMPFutsalTournament.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DUMPFutsalTournament.Domain.Implementations
{
    public class TeamRepository : ITeamRepository
    {
        public TeamRepository(FutsalContext context)
        {
            _context = context;
        }
        private readonly FutsalContext _context;

        public List<Team> GetAllTeams()
        {
            return _context.Teams.ToList();
        }

        public List<Team> GetAllGrouplessTeams()
        {
            return _context.Teams
                .Include(team => team.Group)
                .Where(team => team.Group == null)
                .ToList();
        }

        public Team GetSpecificTeam(int teamId)
        {
            return _context.Teams
                .Include(team => team.Players)
                .ThenInclude(player => player.MatchEvents)
                .SingleOrDefault(team => team.TeamId == teamId);
        }

        public void AddTeam(Team team)
        {
            if (team.Name == null)
                return;
            _context.AttachRange(team.Players);
            _context.Teams.Add(team);
            _context.SaveChanges();
        }

        public void EditTeam(Team editedTeam)
        {
            if (editedTeam.Name == null)
                return;
            _context.AttachRange(editedTeam.Players);
            var teamToEdit = _context.Teams
                .SingleOrDefault(team => team.TeamId == editedTeam.TeamId);
            if (teamToEdit == null)
                return;
            _context.Entry(teamToEdit).Collection(team => team.Players).Load();
            teamToEdit.Name = editedTeam.Name;
            teamToEdit.Players = editedTeam.Players;
            _context.SaveChanges();
        }

        public void DeleteTeam(int teamId)
        {
            var teamToDelete = _context.Teams
                .Include(team => team.HomeMatches)
                .Include(team => team.AwayMatches)
                .FirstOrDefault(team => team.TeamId == teamId);

            if (teamToDelete == null || teamToDelete.HomeMatches.Any() || teamToDelete.AwayMatches.Any())
                return;
            _context.Entry(teamToDelete).Collection(team => team.Players).Load();
            foreach (var player in teamToDelete.Players)
                player.Team = null;
            _context.Remove(teamToDelete);
            _context.SaveChanges();
        }
    }
}