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
        
        [DisplayName("Interface")]
        public string Interface { get; set; }
        
        [DisplayName("Size")]
        public string Size { get; set; }
        
        [DisplayName("Component type")]
        public string Type { get; set; }
        
        [DisplayName("Buffer")]
        public string Buffer { get; set; }
        
        [DisplayName("Speed")]
        public string Speed { get; set; }
        
        [DisplayName("Socket")]
        public string Socket { get; set; }
        
        [DisplayName("Frequency")]
        public string Frequency { get; set; }
        
        [DisplayName("Max read capability")]
        public string MaxRead { get; set; }
        
        [DisplayName("Efficiency")]
        public float Efficiency { get; set; }
        
        [DisplayName("Technology")]
        public int Technology { get; set; }
        
        [DisplayName("Power")]
        public int Power { get; set; }
        
        [DisplayName("Number of cores")]
        [Range(0,16)]
        public int NumberCores { get; set; }
        
        [DisplayName("Number of slots")]
        public int NumberSlots { get; set; }
        
        [DisplayName("Number of fans")]
        public int NumberFans { get; set; }
        
        [DisplayName("Category Type")]
        [Required]
        public int CategoryId { get; set; }
        
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
