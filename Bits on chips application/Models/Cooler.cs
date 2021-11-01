using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bits_on_chips_application.Models
{
    public class Cooler
    {
        [Key]
        public int CoolerId { get; set; }

        [DisplayName("Cooler type")]
        public string Type { get; set; }

        [DisplayName("Rotation speed")]
        public string Speed { get; set; }

        [DisplayName("Number of fans")]
        public int NumberFans { get; set; }

        public int ComponentId { get; set; }
        [ForeignKey("ComponentId")]
        public virtual Component Component { get; set; }
    }
}
