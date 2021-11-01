using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bits_on_chips_application.Models
{
    public class Component
    {
        [Key]
        public int ComponentId { get; set; }
        
        [DisplayName("Component name")]
        public string ComponentName { get; set; }
        
        [DisplayName("Producer")]
        public string Producer { get; set; }
        
        [DisplayName("Price")]
        public float Price { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Quantity")]
        public int Quantity { get; set; }
        
        [DisplayName("Category Type")]
        [Required]
        public int CategoryId { get; set; }
        
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
