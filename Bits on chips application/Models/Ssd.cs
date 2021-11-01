using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bits_on_chips_application.Models
{
    public class Ssd
    {
        [Key]
        public int SsdId { get; set; }

        [DisplayName("Interface")]
        public string Interface { get; set; }

        [DisplayName("Capacity")]
        public string Capacity { get; set; }

        [DisplayName("Max read capability")]
        public string MaxRead { get; set; }

        public int ComponentId { get; set; }
        [ForeignKey("ComponentId")]
        public virtual Component Component { get; set; }
    }
}
