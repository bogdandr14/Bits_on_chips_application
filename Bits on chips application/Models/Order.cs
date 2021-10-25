using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bits_on_chips_application.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        
        [Required]
        [DisplayName("Date ordered")]
        public DateTime Date { get; set; }
        
        [Required]
        [DisplayName("Price")]
        public float Price { get; set; }
        public string ShipmentAddress { get; set; }

        public int? PaymentMethodId { get; set; }
        [ForeignKey("PaymentMethodId")]
        public virtual PaymentMethod PaymentMethod { get; set; }

        public int? ShipmentMethodId { get; set; }
        [ForeignKey("ShipmentMethodId")]
        public virtual ShipmentMethod ShipmentMethod { get; set; }
    }
}
