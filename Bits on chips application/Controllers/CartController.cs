using Bits_on_chips_application.Data;
using Bits_on_chips_application.Models;
using Microsoft.AspNetCore.Identity;
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
        UserManager<ApplicationUser> _userManager;
        public CartController(BitsOnChipsDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public IActionResult ShoppingCart()
        {
            string userId = _userManager.GetUserId(User);
            var obj = _db.DBCarts.Where(obj => obj.Id == userId && obj.OrderId == 1);
            foreach(var item in obj)
            {
                item.Component = _db.DBComponents.Find(item.ComponentId);
                item.Component.Category = _db.DBCategories.Find(item.Component.CategoryId);
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddItem(Component component)
        {
            string userId = _userManager.GetUserId(User);
            component = _db.DBComponents.FirstOrDefault(obj=> obj.ComponentId == component.ComponentId);
            CartItem cartItem = _db.DBCarts.FirstOrDefault(obj => obj.ComponentId == component.ComponentId && obj.Id == userId && obj.OrderId == 1);
            if (cartItem == default(CartItem))
            {
                cartItem = new CartItem
                {
                    ComponentId = component.ComponentId,
                    Id = userId,
                    OrderId = 1,
                    Quantity = 1
                };
                _db.DBCarts.Add(cartItem);
            }
            else
            {
                ++cartItem.Quantity;
                _db.DBCarts.Update(cartItem);
            }
            _db.SaveChanges();
            Category category = _db.DBCategories.FirstOrDefault(obj => obj.CategoryId == component.CategoryId);
            TempData["item added"] = component.ComponentId.ToString();
            return RedirectToAction(category.CategoryName, "Store");
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
