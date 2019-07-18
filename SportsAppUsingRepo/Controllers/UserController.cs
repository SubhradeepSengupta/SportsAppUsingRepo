using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsAppUsingRepo.Models;
using SportsAppUsingRepo.Unit;

namespace SportsAppUsingRepo.Controllers
{
    public class UserController : Controller
    {
        private readonly UnitOfWork uof;

        public UserController(SportsDbContext context)
        {
            uof = new UnitOfWork(context);
        }

        public IActionResult Index()
        {
            return View(uof.User.GetAll());
        }

        public IActionResult Create()
        {
            var EnumValues = Enum.GetValues(typeof(UserRole)).Cast<UserRole>().ToList();
            ViewBag.EnumValue = EnumValues;
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] User user)
        {
            if (ModelState.IsValid)
            {
                uof.User.CreateUser(user);
                uof.Save();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            User user = uof.User.GetUserById(id);
            var EnumValues = Enum.GetValues(typeof(UserRole)).Cast<UserRole>().ToList();
            ViewBag.EnumValue = EnumValues;
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit([FromForm] User user)
        {
            if (ModelState.IsValid)
            {
                uof.User.EditUser(user);
                uof.Save();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            User user = uof.User.GetUserById(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            uof.User.DeleteUser(id);
            uof.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}