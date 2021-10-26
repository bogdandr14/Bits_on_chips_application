using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bits_on_chips_application.Models
{
    public class WishItem
    {
        [Key]
        public int WishItemId { get; set; }

        public int ComponentId { get; set; }
        [ForeignKey("ComponentId")]
        public virtual Component Component { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
