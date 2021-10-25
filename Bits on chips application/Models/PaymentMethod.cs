using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bits_on_chips_application.Models
{
    public class PaymentMethod
    {
        [Key]
        public int PaymentMethodId { get; set; }
        [DisplayName("Payment")]

        public string PaymentName { get; set; }
    }
}
