using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bits_on_chips_application.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [DisplayName("Username")]
        public string Username { get; set; }
        [Required]
        [DisplayName("First name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last name")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("Password")]
        public string Password { get; set; }
        public Contact Contact { get; set; }
    }
}
