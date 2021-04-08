using Bits_on_chips_application.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bits_on_chips_application.Controllers
{
    public class CartController : Controller
    {
        private readonly BitsOnChipsDbContext _db;
        public CartController(BitsOnChipsDbContext db)
        {
            _db = db;
        }
        public IActionResult ShoppingCart()
        {
            var obj = _db.DBCarts;
            return View(obj);
        }
        public IActionResult AddItem()
        {
            return View();
        }
        public IActionResult RemoveItem()
        {
            return View();
        }
        public IActionResult Order()
        {
            return View();
        }
    }
}
