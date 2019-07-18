using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsAppUsingRepo.Models;
using SportsAppUsingRepo.Unit;
using SportsAppUsingRepo.ViewModel;

namespace SportsAppUsingRepo.Controllers
{
    public class TestController : Controller
    {
        private readonly UnitOfWork uof;
        private static int testId = 0;

        public TestController(SportsDbContext context)
        {
            uof = new UnitOfWork(context);
        }

        public IActionResult Index()
        {
            return View(uof.Test.GetAllTests());
        }

        public IActionResult CreateTest()
        {
            return View(uof.Test.GetTestTypes());
        }

        [HttpPost]
        public IActionResult CreateTest([FromForm] TestViewModel test)
        {
            uof.Test.CreateTest(test);
            uof.Save();
            return RedirectToAction("Index");
        }

        public IActionResult TestDetails(int id)
        {
            testId = id;
            return View(uof.Test.TestDetails(id));
        }

        public IActionResult DeleteTest(int id)
        {
            uof.Test.DeleteTest(id);
            uof.Save();
            return RedirectToAction("Index");
        }

        public IActionResult AddAthlete(string testName)
        {
            ViewBag.TestName = testName;
            return View(uof.User.GetAll().Where(u => u.Role.Equals(UserRole.Athlete)));
        }

        [HttpPost]
        public IActionResult AddAthlete([FromForm] UserTestViewModel athlete)
        {
            uof.Test.AddAthlete(athlete);
            uof.Save();
            return RedirectToAction("TestDetails", new { id = testId });
        }

        public IActionResult EditAthlete(int id, string testName)
        {
            ViewBag.TestName = testName;
            return View(uof.Test.EditAthlete(id));
        }

        public IActionResult EditAthleteSave([FromForm] EditAthleteViewModel athlete)
        {
            uof.Test.EditAthleteSave(athlete);
            uof.Save();
            return RedirectToAction("TestDetails", new { id = testId });
        }

        public IActionResult DeleteAthlete(int id, int testID)
        {
            uof.Test.DeleteAthlete(id, testID);
            uof.Save();
            return RedirectToAction("TestDetails", new { id = testId });
        }
    }
}