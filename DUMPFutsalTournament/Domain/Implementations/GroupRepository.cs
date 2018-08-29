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

        public List<ExtendedGroup> GetAllGroupsWithAdditionalData()
        {
            return _context.Groups
                .Include(group => group.Teams)
                    .ThenInclude(team => team.AwayMatches)
                .Include(group => group.Teams)
                    .ThenInclude(team => team.HomeMatches)
                .ToList()
                .Select(group =>
                {
                    var g = new ExtendedGroup();
                    g.Name = group.Name;
                    g.GroupId = group.GroupId;

                    g.Teams = group.Teams.Select(t =>
                    {
                        return new GroupTeam()
                        {
                            TeamId = t.TeamId,
                            TeamName = t.Name,
                            GoalsScored = t.HomeMatches.Sum(m => m.HomeGoals ?? 0) + t.AwayMatches.Sum(m => m.AwayGoals ?? 0),
                            GoalsTaken = t.HomeMatches.Sum(m => m.AwayGoals ?? 0) + t.AwayMatches.Sum(m => m.HomeGoals ?? 0),
                            MatchesPlayed = t.HomeMatches.Count(m => m.AwayGoals.HasValue && m.HomeGoals.HasValue) + t.AwayMatches.Count(m => m.AwayGoals.HasValue && m.HomeGoals.HasValue),
                            Points = t.HomeMatches.Where(m => m.AwayGoals.HasValue && m.HomeGoals.HasValue).Sum(m =>
                            {
                                if (m.AwayGoals == m.HomeGoals) return 1;
                                else if (m.AwayGoals > m.HomeGoals) return 0;
                                else return 3;
                            }) + t.AwayMatches.Where(m => m.AwayGoals.HasValue && m.HomeGoals.HasValue).Sum(m =>
                            {
                                if (m.AwayGoals == m.HomeGoals) return 1;
                                else if (m.AwayGoals > m.HomeGoals) return 3;
                                else return 0;
                            })
                        };
                    }).ToList();
                    return g;
                })
                .ToList();
        }

        public Group GetSpecificGroup(int groupId)
        {
            return _context.Groups
                .Include(group => group.Teams)
                .SingleOrDefault(group => group.GroupId == groupId);
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
                    Team = team,
                    NumberOfGames = teamGroupMatches.Count(),
                    Points = points,
                    GoalsScored = goalsScored,
                    GoalsConceded = goalsConceded
                });
            }

            return calculatedGroupStandings
                .OrderByDescending(standing => standing.Points)
                .ThenByDescending(standing => standing.GoalsScored - standing.GoalsConceded)
                .ThenByDescending(standing => standing.GoalsScored)
                .ThenBy(standing => standing.Team.Name)
                .ToList();
        }

        public void AddGroup(Group group)
        {
            if (group.Size != group.Teams.Count)
                return;
            if (group.Teams.Any(team => GetAllGroups().SelectMany(gr => gr.Teams).Any(t => team.TeamId == t.TeamId)))
                return;
            _context.Teams.AttachRange(group.Teams);
            _context.Groups.Add(group);
            _context.SaveChanges();
        }

        public void EditGroup(Group editedGroup)
        {
            if (editedGroup.Size != editedGroup.Teams.Count)
                return;
            _context.Teams.AttachRange(editedGroup.Teams);
            var groupToEdit = _context.Groups
                .SingleOrDefault(group => group.GroupId == editedGroup.GroupId);
            if (groupToEdit == null)
                return;
            _context.Entry(groupToEdit).Collection(group => group.Teams).Load();
            groupToEdit.Name = editedGroup.Name;
            groupToEdit.Size = editedGroup.Size;
            groupToEdit.Teams = editedGroup.Teams;
            _context.SaveChanges();
        }

        public void DeleteGroup(int groupId)
        {
            var groupToDelete = _context.Groups.Find(groupId);
            if (groupToDelete == null)
                return;
            _context.Entry(groupToDelete).Collection(group => group.Teams).Load();
            foreach (var team in groupToDelete.Teams)
                team.Group = null;
            _context.Groups.Remove(groupToDelete);
            _context.SaveChanges();
        }
    }

    public class GroupTeam
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }

        public int MatchesPlayed { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsTaken { get; set; }
        public int Points { get; set; }
    }

    public class ExtendedGroup
    {
        public int GroupId { get; set; }
        public string Name { get; set; }

        public List<GroupTeam> Teams { get; set; }
    }
}
