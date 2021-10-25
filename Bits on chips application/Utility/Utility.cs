using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bits_on_chips_application.Utility
{
    public static class Helper
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        public static string UpdateSuccess = "The personal information has been successfully updated";
        public static string UpdateWrongPassword = "Passwords do not match!";
        public static string UpdatePasswordMissingReq = "The new password does not meet the requirements!";
        public static string UpdateError = "Could not update with the new information.";
        public static string LoginSuccess = "You have logged in successfully!";
        public static string LoginFailure = "Invalid login attempt";
        public static string LoginUnavailable = "Could not log in";
        public static string UserCreationSuccess = "User created successfully";
        public static string UserCreationFailure = "User could not be created";
        public static string UserAlreadyExist = "The user already exists!";
        public static string LogoutSuccessful = "Log out successfull";
        public static string InvalidInput = "The fields do not met the requirements!";
        public static string InvalidQuantity = "Invalid quantity (range 1-100)";

        public static List<SelectListItem> GetRolesForLogin()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Value=Helper.Admin,Text=Helper.Admin },
                new SelectListItem{Value=Helper.Customer,Text=Helper.Customer }
            };
        }
    }
}
