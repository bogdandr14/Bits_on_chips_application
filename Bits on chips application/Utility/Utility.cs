using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bits_on_chips_application.Utility
{
    public static class Helper
    {
        public static string Admin = "Admin";
        public static string Customer = "Customer";
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
