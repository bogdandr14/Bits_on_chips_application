using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bits_on_chips_application.Models.ViewModels
{
    public class ComponentVM
    {
        public Component Component { get; set; }

        public IEnumerable<SelectListItem> TypeDropDown { get; set; }
    }
}
