using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bits_on_chips_application.Models
{
    public class Source
    {
        [Key]
        public int SourceId { get; set; }
        
        [DisplayName("Power")]
        public int Power { get; set; }

        [DisplayName("Number of fans")]
        public int NumberFans { get; set; }

        [DisplayName("Efficiency")]
        public float Efficiency { get; set; }

        public int ComponentId { get; set; }
        [ForeignKey("ComponentId")]
        public virtual Component Component { get; set; }
    }
}
