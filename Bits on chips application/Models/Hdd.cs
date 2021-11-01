using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bits_on_chips_application.Models
{
    public class Hdd
    {
        [Key]
        public int HddId { get; set; }

        [DisplayName("Interface")]
        public string Interface { get; set; }

        [DisplayName("Capacity")]
        public string Capacity { get; set; }

        [DisplayName("Buffer")]
        public string Buffer { get; set; }

        [DisplayName("Speed")]
        public string Speed { get; set; }

        public int ComponentId { get; set; }
        [ForeignKey("ComponentId")]
        public virtual Component Component { get; set; }
    }
}
