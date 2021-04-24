using OwnThings.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OwnThings.Core.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User Authenticate(string username, string password);
        User CreateUser(string username, string password);
        string GenerateHash(string payload);
        User GetUserByName(string username);
        User GetUserByToken(string token);
        User GetUserById(long id);
    }
}
