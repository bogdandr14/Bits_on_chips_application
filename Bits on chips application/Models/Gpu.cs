using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bits_on_chips_application.Models
{
    public class Gpu
    {
        [Key]
        public int GpuId { get; set; }

        [DisplayName("Interface")]
        public string Interface { get; set; }

        [DisplayName("Capacity")]
        public string Capacity { get; set; }

        [DisplayName("Memory type")]
        public string Type { get; set; }

        [DisplayName("Frequency")]
        public string Frequency { get; set; }

        public int ComponentId { get; set; }
        [ForeignKey("ComponentId")]
        public virtual Component Component { get; set; }
    }
}
