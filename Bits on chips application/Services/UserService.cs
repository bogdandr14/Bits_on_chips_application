using Bits_on_chips_application.Models;
using Bits_on_chips_application.Models.ViewModels;
using Bits_on_chips_application.Utility;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

        public async Task<ApplicationUser> GetUserAsync(System.Security.Claims.ClaimsPrincipal User)
        {
            return await _userManager.GetUserAsync(User);
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
        public async Task<SignInResult> LogInUserAsync(LoginVM loginInfo)
        {
            SignInResult signInResult = await _signInManager.PasswordSignInAsync(loginInfo.Username, loginInfo.Password, loginInfo.RememberMe, false);
            return signInResult;
        }

        public async Task SignOutUser()
        {
            await _signInManager.SignOutAsync();
        }

        public bool IsUserSignedIn(System.Security.Claims.ClaimsPrincipal User)
        {
            return _signInManager.IsSignedIn(User);
        }
    }
}
