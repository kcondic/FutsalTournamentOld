using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DUMPFutsalTournament.Data.Entities;

namespace DUMPFutsalTournament.Domain.Interfaces
{
    public interface ILoginRepository
    {
        User GetByUsername(string username);
        bool AddUser(User userToAdd);
    }
}
