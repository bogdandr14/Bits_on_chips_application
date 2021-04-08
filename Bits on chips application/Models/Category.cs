using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bits_on_chips_application.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        [DisplayName("Category name")]
        public string CategoryName { get; set; }
    }
}
