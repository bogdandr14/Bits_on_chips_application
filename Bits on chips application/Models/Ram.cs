using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bits_on_chips_application.Models
{
    public class Ram
    {
        [Key]
        public int RamId { get; set; }

        [DisplayName("Ram type")]
        public string Type { get; set; }

        [DisplayName("Capacity")]
        public string Capacity { get; set; }

        [DisplayName("Frequency")]
        public string Frequency { get; set; }
        
        [DisplayName("CAS latency")]
        [Range(10, 24)]
        public int Latency { get; set; }

        public int ComponentId { get; set; }
        [ForeignKey("ComponentId")]
        public virtual Component Component { get; set; }
    }
}
