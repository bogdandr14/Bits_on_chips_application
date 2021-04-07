using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bits_on_chips_application.Models
{
    public class Contact
    {
        [Key]
        public int ContactID { get; set; }
        [Required]
        [DisplayName("Home address")]
        public string Address { get; set; }
        [Required]
        [DisplayName("E-mail")]
        public string Email { get; set; }
        [DisplayName("Phone number")]
        public string Phone { get; set; }
        public User User { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
    }
}
