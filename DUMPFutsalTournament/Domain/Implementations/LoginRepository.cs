using System.Linq;
using DUMPFutsalTournament.Data;
using DUMPFutsalTournament.Data.Entities;
using DUMPFutsalTournament.Domain.Interfaces;

namespace DUMPFutsalTournament.Domain.Implementations
{
    public class LoginRepository : ILoginRepository
    {
        public LoginRepository(FutsalContext context)
        {
            _context = context;
        }
        private readonly FutsalContext _context;

        public User GetByUsername(string username)
        {
            return _context.Users.SingleOrDefault(user => user.Username == username);
        }

        public User GetById(int id)
        {
            return _context.Users.SingleOrDefault(user => user.UserId == id);
        }

        public void ChangePassword(User userToChangeFor, string password)
        {
            userToChangeFor.Password = password;
            _context.SaveChanges();
        }

        public bool AddUser(User userToAdd)
        {
            if (GetByUsername(userToAdd.Username) != null)
                return false;
            _context.Users.Add(userToAdd);
            _context.SaveChanges();
            return true;
        }
    }
}
