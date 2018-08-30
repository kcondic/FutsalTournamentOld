using System.Collections.Generic;
using DUMPFutsalTournament.Data.Entities;
using DUMPFutsalTournament.Domain.HelperClasses;
using DUMPFutsalTournament.Domain.Implementations;

namespace DUMPFutsalTournament.Domain.Interfaces
{
    public interface IGroupRepository
    {
        List<Group> GetAllGroups();
        Group GetSpecificGroup(int groupId);
        List<ExtendedGroup> GetCalculatedGroupStandings();
        void AddGroup(Group group);
        void EditGroup(Group editedGroup);
        void DeleteGroup(int groupId);
    }
}
