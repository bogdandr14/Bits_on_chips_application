using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bits_on_chips_application.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int CartItemId { get; set; }
        public User User { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public Component Component { get; set; }
        [ForeignKey("Component")]
        public int ComponentId { get; set; }
        public int Count { get; set; }
    }
}
