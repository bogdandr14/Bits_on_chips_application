    using Bits_on_chips_application.Data;
using Bits_on_chips_application.Models;
using Bits_on_chips_application.Models.ViewModels;
using Bits_on_chips_application.Utility;
using Microsoft.AspNetCore.Identity;
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
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;
        public UserController(BitsOnChipsDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser>signInManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
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
        public async Task<IActionResult> Register()
        {
            if (!_roleManager.RoleExistsAsync(Helper.Admin).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole(Helper.Admin));
                await _roleManager.CreateAsync(new IdentityRole(Helper.Customer));

            }
            return View();
        }
/*        //Post-Register
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
        }*/

        //Post-Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM  obj)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = obj.Username,
                    FirstName = obj.FirstName,
                    LastName = obj.LastName,
                    BirthDate = obj.BirthDate,
                    Address = obj.Address,
                    Email = obj.Email,
                    PhoneNumber = obj.Phone
                };
                var result = await _userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Helper.Customer);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(obj);
        }
        public IActionResult Change()
        {
            return View();
        }
    }
}
