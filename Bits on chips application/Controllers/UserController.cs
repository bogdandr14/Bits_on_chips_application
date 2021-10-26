using Bits_on_chips_application.Models;
using Bits_on_chips_application.Models.ViewModels;
using Bits_on_chips_application.Services;
using Bits_on_chips_application.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Bits_on_chips_application.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        [Route("User/Info")]
        public async Task<IActionResult> InfoAsync()
        {
            string username = HttpContext.Items["Username"].ToString();
            ApplicationUser user = await _userService.GetUserAsync(username);
            return View(user);
        }

        [Route("User/Login")]
        [Route("User/SignIn")]
        public IActionResult Login()
        {
            if (_userService.IsUserSignedIn(HttpContext))
            {
                return RedirectToAction("Info");
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
                UserResponseVM userResponse = await _userService.LogInUserAsync(model);
                if (userResponse != null)
                {
                    Response.Cookies.Append("Authorization", userResponse.Token);

                    TempData["message"] = Helper.LoginSuccess;
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", Helper.LoginFailure);
            }
            TempData["message"] = Helper.LoginUnavailable;
            return View(model);
        }

        //Get-Register
        [HttpGet]
        [Route("User/Register")]
        [Route("User/SignUp")]
        public IActionResult Register()
        {
            if (_userService.IsUserSignedIn(HttpContext))
            {
                return RedirectToAction("Info");
            }
            return View();
        }

        //Post-Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("User/Register")]
        public async Task<IActionResult> Register(RegisterVM obj)
        {
            /* string returnUrl = Url.Content("~/");
             List<Microsoft.AspNetCore.Authentication.AuthenticationScheme> authenticationSchemes = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();*/
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(obj.PhotoFile.FileName);
                string extension = Path.GetExtension(obj.PhotoFile.FileName);
                obj.PhotoName = fileName + DateTime.Now.ToString("yyMMddHHmmss") + extension;
                IdentityResult identityResult = await _userService.RegisterUserAsync(obj);
                using (Stream fileStream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\ProfilePictures", obj.PhotoName), FileMode.Create))
                {
                    await obj.PhotoFile.CopyToAsync(fileStream);
                }
                if (identityResult != null && identityResult.Succeeded)
                {
                    TempData["message"] = Helper.UserCreationSuccess;
                    return RedirectToAction("Index", "Home");
                }
                if (identityResult == null)
                {
                    TempData["message"] = Helper.UserAlreadyExist;
                }
                else
                {
                    foreach (var err in identityResult.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
            }
            TempData["message"] = Helper.UserCreationFailure;
            return View(obj);
        }

        [Authorize]
        [HttpPost]
        [Route("User/LogOff")]
        public async Task<IActionResult> LogOff()
        {
            Response.Cookies.Delete("Authorization");
            TempData["message"] = Helper.LogoutSuccessful;
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [Route("User/Change")]
        [Route("User/ChangeInfo")]
        public async Task<IActionResult> ChangeAsync()
        {
            string username = HttpContext.Items["Username"].ToString();
            ApplicationUser user = await _userService.GetUserAsync(username);

            var obj = new EditVM
            {
                Address = user.Address,
                Email = user.Email,
                Phone = user.PhoneNumber,
                PhotoPath = user.ProfilePicture
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
                string username = HttpContext.Items["Username"].ToString();
                string currentProfilePhoto = (await _userService.GetUserAsync(username)).ProfilePicture;
                if (currentProfilePhoto == null)
                {
                    currentProfilePhoto = "";
                }
                if (modifications.PhotoFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(modifications.PhotoFile.FileName);
                    string extension = Path.GetExtension(modifications.PhotoFile.FileName);
                    modifications.PhotoPath = fileName + DateTime.Now.ToString("yyMMddHHmmss") + extension;
                    TempData["message"] = await _userService.UpdateUserAsync(modifications, User);
                }
                else
                {
                    TempData["message"] = await _userService.UpdateUserAsync(modifications, User);
                }
                if (TempData["message"].ToString().Equals(Helper.UpdateSuccess))
                {
                    if (modifications.PhotoFile != null)
                    {
                        string fullPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\ProfilePictures", currentProfilePhoto);
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                        using (Stream fileStream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\ProfilePictures", modifications.PhotoPath), FileMode.Create))
                        {
                            await modifications.PhotoFile.CopyToAsync(fileStream);
                        }
                    }
                    return RedirectToAction("Info", "User");
                }
            }
            else
            {
                TempData["message"] = Helper.InvalidInput;
            }
            return RedirectToAction("Change");
        }
    }
}
