using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bits_on_chips_application.Models
{
    public class Cpu
    {
        [Key]
        public int CpuId { get; set; }

        [DisplayName("Socket")]
        public string Socket { get; set; }

        [DisplayName("Frequency")]
        public string Frequency { get; set; }

        [DisplayName("Technology")]
        public int Technology { get; set; }

        [DisplayName("Number of cores")]
        [Range(0, 16)]
        public int NumberCores { get; set; }

        public int ComponentId { get; set; }
        [ForeignKey("ComponentId")]
        public virtual Component Component { get; set; }
    }
}
