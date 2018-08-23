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
    public class GroupRepository : IGroupRepository
    {
        public GroupRepository(FutsalContext context)
        {
            _context = context;
        }
        private readonly FutsalContext _context;

        public List<Group> GetAllGroups()
        {
            return _context.Groups
                .Include(group => group.Teams)
                .ToList();
        }

        public List<GroupStanding> GetCalculatedGroupStandings(int groupId)
        {
            var calculatedGroupStandings = new List<GroupStanding>();
            var groupTeams = _context.Teams
                .Include(team => team.Group)
                .Where(team => team.Group.GroupId == groupId);

            foreach (var team in groupTeams)
            {
                var points = 0;
                var goalsScored = 0;
                var goalsConceded = 0;
                var teamGroupMatches = _context.Matches
                    .Include(match => match.HomeTeam)
                    .Include(match => match.AwayTeam)
                    .Where(match =>
                            match.MatchType == MatchType.Group &&
                            (match.HomeTeam.TeamId == team.TeamId || match.AwayTeam.TeamId == team.TeamId));

                foreach (var match in teamGroupMatches)
                {
                    if (match.HomeGoals == match.AwayGoals)
                    {
                        points += 1;
                        goalsScored += match.HomeGoals ?? 0;
                        goalsConceded += match.AwayGoals ?? 0;
                    }
                    else if (match.HomeTeam.TeamId == team.TeamId)
                    {
                        if (match.HomeGoals > match.AwayGoals)
                            points += 3;
                        goalsScored += match.HomeGoals ?? 0;
                        goalsConceded += match.AwayGoals ?? 0;
                    }
                    else
                    {
                        if (match.AwayGoals > match.HomeGoals)
                            points += 3;
                        goalsScored += match.AwayGoals ?? 0;
                        goalsConceded += match.HomeGoals ?? 0;
                    }
                }

                calculatedGroupStandings.Add(new GroupStanding
                {
                    NumberOfGames = teamGroupMatches.Count(),
                    Points = points,
                    GoalsScored = goalsScored,
                    GoalsConceded = goalsConceded
                });
            }

            return calculatedGroupStandings;
        }

        public void AddGroup(Group group)
        {
            foreach (var team in group.Teams)
                _context.Teams.Attach(team);
            _context.Groups.Add(group);
            _context.SaveChanges();
        }

        public void EditGroup(Group editedGroup)
        {
            var groupToEdit = _context.Groups
                .Include(group => group.Teams)
                .FirstOrDefault(group => group.GroupId == editedGroup.GroupId);
            if (groupToEdit == null)
                return;
            foreach (var player in editedGroup.Teams)
                _context.Attach(player);
            groupToEdit.Name = editedGroup.Name;
            groupToEdit.Teams = editedGroup.Teams;
            _context.SaveChanges();
        }

        public void DeleteGroup(int groupId)
        {
            var groupToDelete = _context.Groups.Find(groupId);
            if (groupToDelete == null)
                return;
            _context.Groups.Remove(groupToDelete);
            _context.SaveChanges();
        }
    }
}
