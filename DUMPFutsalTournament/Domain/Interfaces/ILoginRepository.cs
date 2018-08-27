using DUMPFutsalTournament.Data.Entities;

namespace DUMPFutsalTournament.Domain.Interfaces
{
    public interface ILoginRepository
    {
        User GetByUsername(string username);
        User GetById(int id);
        void ChangePassword(User user, string password);
        bool AddUser(User userToAdd);
    }
}
