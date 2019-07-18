using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportsAppUsingRepo.Models;
using SportsAppUsingRepo.ViewModel;

namespace SportsAppUsingRepo.Repositories.TestRepo
{
    public class TestRepository : ITestRepository
    {
        private readonly SportsDbContext context;
        private static int TestId = 0;

        public TestRepository(SportsDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return context.Users.ToList();
        }

        public IEnumerable<TestTypeMapper> GetAllTests()
        {
            return context.TestTypeMappers.Include(t => t.Test).ThenInclude(t=>t.UserTestMappers).Include(t => t.TestType).ToList();
        }

        public IEnumerable<TestType> GetTestTypes()
        {
            return context.TestTypes.ToList();
        }

        public void CreateTest(TestViewModel test)
        {
            TestType TestTypes = context.TestTypes.FirstOrDefault(t => t.Name == test.TestType);
            TestTypeMapper TestType = new TestTypeMapper();
            Test Tests = new Test();

            Tests.Date = test.Date;
            context.Tests.Add(Tests);

            TestType.TestID = Tests.ID;
            TestType.TestTypeID = TestTypes.ID;
            context.TestTypeMappers.Add(TestType);
        }

        public TestTypeMapper TestDetails(int id)
        {
            TestId = id;
            return context.TestTypeMappers.Include(t => t.Test).ThenInclude(t => t.UserTestMappers).ThenInclude(t => t.User).Include(t => t.Test.TestTypeMapper.TestType).Where(t => t.TestID == id).FirstOrDefault();
        }

        public void DeleteTest(int id)
        {
            Test test = context.Tests.Find(id);
            context.Tests.Remove(test);
        }

        public void AddAthlete(UserTestViewModel user)
        {
            UserTestMapper UserTest = new UserTestMapper();
            var UserExist = context.UserTestMappers.Where(t => t.TestID == TestId).Select(u => u.User.Name).ToList();
            int User = context.Users.Where(u => u.Name == user.Name).Select(u => u.ID).FirstOrDefault();

            if (!UserExist.Contains(user.Name))
            {
                UserTest.TestID = TestId;
                UserTest.UserID = User;
                if(user.Distance != null)
                {
                    UserTest.CooperTestDistance = user.Distance;
                    UserTest.FitnessRating = CalculateFitness(user.Distance);
                }
                else
                {
                    UserTest.SprintTestTime = user.Time;
                }
                context.UserTestMappers.Add(UserTest);
            }
            else
            {
                var UpdateUser = context.UserTestMappers.Where(u => u.User.Name == user.Name).Where(u => u.TestID == TestId).FirstOrDefault();

                if (user.Distance != null)
                {
                    UpdateUser.CooperTestDistance = user.Distance;
                    UpdateUser.FitnessRating = CalculateFitness(user.Distance);
                }
                else
                {
                    UpdateUser.SprintTestTime = user.Time;
                }
                context.UserTestMappers.Update(UpdateUser);
            }
        }

        public IEnumerable<UserTestMapper> EditAthlete(int id)
        {
            return context.UserTestMappers.Include(t => t.Test.TestTypeMapper.TestType).Include(t => t.User).Where(t => t.UserID == id).AsEnumerable();            
        }

        public void EditAthleteSave(EditAthleteViewModel athlete)
        {
            var UserPerTest = context.UserTestMappers.Where(u => u.TestID == athlete.TestId).Where(u => u.UserID == athlete.AthleteId).FirstOrDefault();
            if (athlete.CooperDistance != 0)
            {
                UserPerTest.CooperTestDistance = athlete.CooperDistance;
                UserPerTest.FitnessRating = CalculateFitness(athlete.CooperDistance);
            }
            else
            {
                UserPerTest.SprintTestTime = athlete.SprintTime;
            }
            context.UserTestMappers.Update(UserPerTest);
        }

        public void DeleteAthlete(int athleteId, int testId)
        {
            var UserPertest = context.UserTestMappers.Where(t => t.TestID == testId).Where(u => u.UserID == athleteId).FirstOrDefault();
            context.UserTestMappers.Remove(UserPertest);
        }

        private string CalculateFitness(double? distance)
        {
            if (distance <= 1000)
            {
                return "Below Average";
            }
            else if (distance > 1000 && distance <= 2000)
            {
                return "Average";
            }
            else if (distance > 2000 && distance <= 3500)
            {
                return "Good";
            }
            else if (distance > 3500)
            {
                return "Very Good";
            }
            return " ";
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
