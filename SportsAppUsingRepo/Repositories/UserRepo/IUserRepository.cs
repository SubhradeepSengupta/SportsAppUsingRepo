using SportsAppUsingRepo.Models;
using System;
using System.Collections.Generic;

namespace SportsAppUsingRepo.Repositories.UserRepo
{
    interface IUserRepository : IDisposable
    {
        IEnumerable<User> GetAll();
        User GetUserById(int id);
        void CreateUser(User user);
        void DeleteUser(int userId);
        void EditUser(User user);
        void Save();
    }
}
