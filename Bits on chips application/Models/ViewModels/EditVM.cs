using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bits_on_chips_application.Models.ViewModels
{
    public class EditVM
    {
        [DisplayName("New Password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The string {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string NewPassword { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The string {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DisplayName("Current Password")]
        public string ConfirmPassword { get; set; }
        
        [DisplayName("Home address")]
        public string Address { get; set; }
        
        [Required]
        [EmailAddress]
        [DisplayName("E-mail")]
        public string Email { get; set; }
        
        [DisplayName("Phone number")]
        public string Phone { get; set; }
    }
}
