using Bits_on_chips_application.Data;
using Bits_on_chips_application.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bits_on_chips_application.Controllers
{
    public class UserController : Controller
    {
        private readonly BitsOnChipsDbContext _db;
        public UserController(BitsOnChipsDbContext db)
        {
            _db = db;
        }
        public IActionResult Info()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        //Get-Register
        public IActionResult Register()
        {
            return View();
        }
        //Post-Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User obj)
        {
            if (ModelState.IsValid)
            {
                _db.DBUsers.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Info");
            }
            return View(obj);
        }
        public IActionResult Change()
        {
            return View();
        }
    }
}
