using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bits_on_chips_application.Models.ViewModels
{
    public class PaymentVM
    {
        [Required]
        [DisplayName("Price")]
        public float Price { get; set; }
        [DisplayName("Delivery address")]
        public string ShipmentAddress { get; set; }

        [Required]
        [DisplayName("Payment method")]
        public int? PaymentMethodId { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }

        [Required]
        [DisplayName("Delivery method")]
        public int? ShipmentMethodId { get; set; }
        public virtual ShipmentMethod ShipmentMethod { get; set; }
        public IEnumerable<SelectListItem> PaymentDropDown { get; set; }
        public IEnumerable<SelectListItem> ShipmentDropDown { get; set; }

    }
}
