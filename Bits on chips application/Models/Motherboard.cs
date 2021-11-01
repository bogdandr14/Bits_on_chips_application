using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bits_on_chips_application.Models
{
    public class Motherboard
    {
        [Key]
        public int MotherboardId { get; set; }

        [DisplayName("Format")]
        public string Format { get; set; }

        [DisplayName("Socket")]
        public string Socket { get; set; }

        [DisplayName("Interface")]
        public string Interface { get; set; }

        [DisplayName("Memory type")]
        public string Type { get; set; }

        public int ComponentId { get; set; }
        [ForeignKey("ComponentId")]
        public virtual Component Component { get; set; }
    }
}
