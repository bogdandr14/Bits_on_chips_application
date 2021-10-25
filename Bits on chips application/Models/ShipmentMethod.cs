using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bits_on_chips_application.Models
{
    public class ShipmentMethod
    {
        [Key]
        public int ShipmentId { get; set; }
        [DisplayName("Shipment method")]

        public string ShipmentMethodName { get; set; }
    }
}
