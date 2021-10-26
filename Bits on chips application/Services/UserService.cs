using Bits_on_chips_application.Models;
using Bits_on_chips_application.Models.ViewModels;
using Bits_on_chips_application.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Authentication.Jwt;
using Microsoft.AspNetCore.Http;

namespace Bits_on_chips_application.Services
{
    public class UserService : BaseService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserService(IRepositoryWrapper repositoryWrapper, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
            : base(repositoryWrapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ApplicationUser> GetUserAsync(string User)
        {
            return await _userManager.FindByNameAsync(User);
        }


        public async Task<IdentityResult> RegisterUserAsync(RegisterVM registerInfo)
        {
            if (!_roleManager.RoleExistsAsync(Helper.Admin).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole(Helper.Admin));
                await _roleManager.CreateAsync(new IdentityRole(Helper.Customer));
            }
            var user = new ApplicationUser
            {
                UserName = registerInfo.Username,
                FirstName = registerInfo.FirstName,
                LastName = registerInfo.LastName,
                BirthDate = registerInfo.BirthDate,
                Address = registerInfo.Address,
                Email = registerInfo.Email,
                PhoneNumber = registerInfo.Phone,
                ProfilePicture = registerInfo.PhotoName
            };
            var result = await _userManager.CreateAsync(user, registerInfo.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Helper.Customer);
                JwtMiddleware.GenerateToken(user, await _userManager.GetRolesAsync(user));
                await _signInManager.SignInAsync(user, isPersistent: false);
            }
            return result;
        }

        public async Task<string> UpdateUserAsync(EditVM modifications, System.Security.Claims.ClaimsPrincipal User)
        {
            ApplicationUser user = _userManager.GetUserAsync(User).Result;
            Task<bool> task = _userManager.CheckPasswordAsync(user, modifications.ConfirmPassword);
            if (task.Result)
            {
                if (modifications.NewPassword != null)
                {
                    var task1 = await _userManager.ChangePasswordAsync(user, modifications.ConfirmPassword, modifications.NewPassword);
                    if (!task1.Succeeded)
                    {
                        return Helper.UpdatePasswordMissingReq;
                    }
                }
                user.Email = modifications.Email;
                user.Address = modifications.Address;
                user.PhoneNumber = modifications.Phone;
                if(modifications.PhotoFile != null)
                {
                    user.ProfilePicture = modifications.PhotoPath;
                }
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Helper.UpdateSuccess;
                }
                return Helper.UpdateError;
            }
            return Helper.UpdateWrongPassword;
        }
        public async Task<UserResponseVM> LogInUserAsync(LoginVM loginInfo)
        {
            var user = await _userManager.FindByNameAsync(loginInfo.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginInfo.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                return JwtMiddleware.GenerateToken(user, userRoles);
            }
            return null;
        }

        public string GetUserId(HttpContext context)
        {
            return JwtMiddleware.getUserId(context);
        }

        public async Task SignOutUser()
        {
            await _signInManager.SignOutAsync();
        }

        public bool IsUserSignedIn(HttpContext context)
        {
            return JwtMiddleware.IsUserLoggedId(context);
        }
    }
}
