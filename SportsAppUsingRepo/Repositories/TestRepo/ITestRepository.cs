using SportsAppUsingRepo.Models;
using SportsAppUsingRepo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsAppUsingRepo.Repositories.TestRepo
{
    interface ITestRepository : IDisposable
    {
        IEnumerable<User> GetAllUsers();
        IEnumerable<TestTypeMapper> GetAllTests();
        IEnumerable<TestType> GetTestTypes();
        void CreateTest(TestViewModel test);
        TestTypeMapper TestDetails(int id);
        void DeleteTest(int id);
        void AddAthlete(UserTestViewModel athlete);
        IEnumerable<UserTestMapper> EditAthlete(int id);
        void EditAthleteSave(EditAthleteViewModel athlete);
        void DeleteAthlete(int athleteId, int testId);
    }
}
