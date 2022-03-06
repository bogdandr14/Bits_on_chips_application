using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bits_on_chips_application.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [DisplayName("Category name")]

        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
