using Bits_on_chips_application.Data;
using Bits_on_chips_application.Models;
using Bits_on_chips_application.Models.ViewModels;
using Bits_on_chips_application.Services;
using Bits_on_chips_application.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bits_on_chips_application.Controllers
{
    public class UserController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, RoleManager<IdentityRole> roleManager, UserService userService)
        {
            _roleManager = roleManager;
            _userService = userService;
            _logger = logger;
        }
        
        [Authorize]
        [HttpGet]
        [Route("User/Info")]
        public async Task<IActionResult> InfoAsync()
        {
            ApplicationUser user = await _userService.GetUserAsync(User);
            return View(user);
        }

        [Route("User/Login")]
        [Route("User/SignIn")]
        public IActionResult Login()
        {
            if (_userService.IsUserSignedIn(User))
            {
                return RedirectToAction("User//Info");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("User/Login")]
        [Route("User/SignIn")]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                if ((await _userService.LogInUserAsync(model)).Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid login attempt");
            }
            return View(model);
        }

        //Get-Register
        [HttpGet]
        [Route("User/Register")]
        [Route("User/SignUp")]
        public async Task<IActionResult> Register()
        {
            if (_userService.IsUserSignedIn(User))
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
        [Route("User/Register")]
        public async Task<IActionResult> Register(RegisterVM  obj)
        {
           /* string returnUrl = Url.Content("~/");
            List<Microsoft.AspNetCore.Authentication.AuthenticationScheme> authenticationSchemes = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();*/
            if (ModelState.IsValid)
            {
                IdentityResult identityResult = await _userService.RegisterUserAsync(obj);
                if (identityResult.Succeeded) 
                { 
                    return RedirectToAction("Index", "Home");
                }
                foreach(var err in identityResult.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(obj);
        }

        [Authorize]
        [HttpPost]
        [Route("User/LogOff")]
        public async Task<IActionResult> LogOff()
        {
            _userService.SignOutUser();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [Route("User/Change")]
        [Route("User/ChangeInfo")]
        public async Task<IActionResult> ChangeAsync()
        {
            ApplicationUser user = await _userService.GetUserAsync(User);
            var obj = new EditVM
            {
                Address = user.Address,
                Email = user.Email,
                Phone = user.PhoneNumber
            };      
            return View(obj);
        }

        //Post-Register
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("User/ChangePost")]
        public async Task<IActionResult> ChangePost(EditVM modifications)
        {
            if (ModelState.IsValid)
            {
                TempData["message"] =  await _userService.UpdateUserAsync(modifications, User);
                if (TempData["message"].ToString().Equals(Helper.UpdateSuccess))
                {
                    return RedirectToAction("Info", "User");
                }
            }
            else
            {
                TempData["message"] = "The fields do not met the requirements!";
            }
            return RedirectToAction("Change");
        }
    }
}
