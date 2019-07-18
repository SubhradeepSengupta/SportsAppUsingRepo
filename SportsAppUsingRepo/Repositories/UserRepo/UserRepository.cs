using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportsAppUsingRepo.Models;

namespace SportsAppUsingRepo.Repositories.UserRepo
{
    public class UserRepository : IUserRepository
    {
        private readonly SportsDbContext context;

        public UserRepository(SportsDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return context.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return context.Users.Find(id);
        }

        public void CreateUser(User user)
        {
            context.Users.Add(user);
        }

        public void EditUser(User user)
        {
            context.Users.Update(user);
        }

        public void DeleteUser(int userId)
        {
            User user = context.Users.Find(userId);
            context.Users.Remove(user);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
