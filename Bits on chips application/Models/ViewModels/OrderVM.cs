using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bits_on_chips_application.Models.ViewModels
{
    public class OrderVM
    {
        public Order Order { get; set; }
        public IList<CartItem> OrderedItems { get; set; }
    }
}
