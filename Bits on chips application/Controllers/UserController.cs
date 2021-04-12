﻿using Bits_on_chips_application.Data;
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
            ApplicationUser user = _userManager.GetUserAsync(User).Result;
            return View(user);
        }
        public IActionResult Login()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Info");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid login attempt");
            }
            return View(model);
        }
        //Get-Register
        public async Task<IActionResult> Register()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Info");
            }
            if (!_roleManager.RoleExistsAsync(Helper.Admin).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole(Helper.Admin));
                await _roleManager.CreateAsync(new IdentityRole(Helper.Customer));
            }
            return View();
        }

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
                var result = await _userManager.CreateAsync(user, obj.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Helper.Customer);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach(var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(obj);
        }
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Change()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login");
            }
            ApplicationUser user = _userManager.GetUserAsync(User).Result;
           
            var obj = new EditVM
            {
                Address = user.Address,
                Email = user.Email,
                Phone = user.PhoneNumber
            };      
            return View(obj);
        }

        //Post-Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePost(EditVM modifications)
        {
            ApplicationUser user = _userManager.GetUserAsync(User).Result;
            if (ModelState.IsValid)
            {
                Task<bool> task = _userManager.CheckPasswordAsync(user, modifications.ConfirmPassword);
                if (task.Result)
                {
                    if (modifications.NewPassword != null)
                    {
                        var task1 = await _userManager.ChangePasswordAsync(user, modifications.ConfirmPassword, modifications.NewPassword);
                        if (!task1.Succeeded)
                        {
                            TempData["message"] = "The new password does not meet the requirements!";
                            return RedirectToAction("Change");
                        }
                    }
                    user.Email = modifications.Email;
                    user.Address = modifications.Address;
                    user.PhoneNumber = modifications.Phone;
                    var result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        TempData["message"] = "Could not update with the new information.";
                        return RedirectToAction("Change");
                    }
                    return RedirectToAction("Info", "User");
                }
                TempData["message"] = "Passwords do not match!";
                return RedirectToAction("Change");
            }
            TempData["message"] = "The fields do not met the requirements!";
            return RedirectToAction("Change");
        }
    }
}
