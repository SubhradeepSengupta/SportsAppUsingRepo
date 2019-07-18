using SportsAppUsingRepo.Models;
using SportsAppUsingRepo.Repositories.TestRepo;
using SportsAppUsingRepo.Repositories.UserRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsAppUsingRepo.Unit
{
    public class UnitOfWork
    {
        private readonly SportsDbContext context;
        private UserRepository user;
        private TestRepository test;

        public UnitOfWork(SportsDbContext context)
        {
            this.context = context;
        }

        public UserRepository User
        {
            get
            {
                if(user == null)
                {
                    user = new UserRepository(context);
                }
                return user;
            }
        }

        public TestRepository Test
        {
            get
            {
                if (test == null)
                {
                    test = new TestRepository(context);
                }
                return test;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
