using System.Collections.Generic;
using DUMPFutsalTournament.Data.Entities;
using DUMPFutsalTournament.Domain.HelperClasses;

namespace DUMPFutsalTournament.Domain.Interfaces
{
    public interface IGroupRepository
    {
        List<Group> GetAllGroups();
        List<GroupStanding> GetCalculatedGroupStandings(int groupId);
        void AddGroup(Group group);
        void EditGroup(Group editedGroup);
        void DeleteGroup(int groupId);
    }
}
