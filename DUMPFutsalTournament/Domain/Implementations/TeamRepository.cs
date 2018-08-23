using System;
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

        public Team GetSpecificTeam(int teamId)
        {
            return _context.Teams
                .Include(team => team.Players)
                .SingleOrDefault(team => team.TeamId == teamId);
        }

        public void AddTeam(Team team)
        {
            foreach (var player in team.Players)
                _context.Players.Attach(player);
            _context.Teams.Add(team);
            _context.SaveChanges();
        }

        public void EditTeam(Team editedTeam)
        {
            var teamToEdit = _context.Teams
                .Include(team => team.Players)
                .FirstOrDefault(team => team.TeamId == editedTeam.TeamId);
            if (teamToEdit == null)
                return;
            foreach (var player in editedTeam.Players)
                _context.Attach(player);
            teamToEdit.Name = editedTeam.Name;
            teamToEdit.Players = editedTeam.Players;
            teamToEdit.Group = editedTeam.Group;
            _context.SaveChanges();
        }

        public void DeleteTeam(int teamId)
        {
            var teamToDelete = _context.Teams.Find(teamId);
            _context.Remove(teamToDelete);
            _context.SaveChanges();
        }

        public List<Team> GetRandomUngroupedTeams(int numberOfTeams)
        {
            var ungroupedTeams = _context.Teams
                .Include(team => team.Group)
                .Where(team => team.Group == null);

            return ungroupedTeams
                .OrderBy(team => Guid.NewGuid())
                .Take(numberOfTeams)
                .ToList();
        }
    }
}
