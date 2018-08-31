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

        public List<GroupWithStandings> GetCalculatedGroupStandings()
        {
            return _context.Groups
                .Include(group => group.Teams)
                    .ThenInclude(team => team.AwayMatches)
                .Include(group => group.Teams)
                    .ThenInclude(team => team.HomeMatches)
                .ToList()
                .Select(group =>
                {
                    var extendedGroup = new GroupWithStandings()
                    {
                        Group = group,
                        GroupStandings = group.Teams.Select(t =>
                        {
                            return new GroupStanding()
                            {
                                Team = t,
                                GoalsScored =
                                    t.HomeMatches.Sum(m => m.HomeGoals ?? 0) + t.AwayMatches.Sum(m => m.AwayGoals ?? 0),
                                GoalsConceded =
                                    t.HomeMatches.Sum(m => m.AwayGoals ?? 0) + t.AwayMatches.Sum(m => m.HomeGoals ?? 0),
                                MatchesPlayed =
                                    t.HomeMatches.Count(m => m.AwayGoals.HasValue && m.HomeGoals.HasValue) +
                                    t.AwayMatches.Count(m => m.AwayGoals.HasValue && m.HomeGoals.HasValue),
                                Points = t.HomeMatches.Where(m => m.AwayGoals.HasValue && m.HomeGoals.HasValue).Sum(m => m.AwayGoals == m.HomeGoals ? (int)MatchPoints.Draw : m.AwayGoals > m.HomeGoals ? (int)MatchPoints.Lose : (int)MatchPoints.Win) 
                                + t.AwayMatches.Where(m => m.AwayGoals.HasValue && m.HomeGoals.HasValue).Sum(m => m.AwayGoals == m.HomeGoals ? (int)MatchPoints.Draw : m.AwayGoals > m.HomeGoals ? (int)MatchPoints.Win : (int)MatchPoints.Lose)
                            };
                        })
                        .OrderByDescending(t => t.Points)
                        .ThenByDescending(t => t.GoalsScored - t.GoalsConceded)
                        .ThenByDescending(t => t.GoalsScored)
                        .ThenBy(t => t.Team.Name)
                        .ToList()
                    };
                    return extendedGroup;
                })
                .ToList();
        }

        public Group GetSpecificGroup(int groupId)
        {
            return _context.Groups
                .Include(group => group.Teams)
                .SingleOrDefault(group => group.GroupId == groupId);
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
}