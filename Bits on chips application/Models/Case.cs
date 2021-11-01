using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bits_on_chips_application.Models
{
    public class Case
    {
        [Key]
        public int CaseId { get; set; }

        [DisplayName("Size")]
        public string Size { get; set; }

        [DisplayName("Number of slots")]
        public int NumberSlots { get; set; }

        public int ComponentId { get; set; }
        [ForeignKey("ComponentId")]
        public virtual Component Component { get; set; }
    }
}
