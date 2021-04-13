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
        SignInManager<ApplicationUser> _signInManager;
        public CartController(BitsOnChipsDbContext db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
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
        public IActionResult ChangeQuantity(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            CartItem item = _db.DBCarts.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            item.Component = _db.DBComponents.Find(item.ComponentId);
            item.Component.Category = _db.DBCategories.Find(item.Component.CategoryId);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeQuantity(CartItem item)
        {
            if (ModelState.IsValid)
            {
                CartItem cartItem = _db.DBCarts.Find(item.CartItemId);
                if (cartItem == null)
                {
                    return NotFound();
                }
                cartItem.Quantity = item.Quantity;
                _db.DBCarts.Update(cartItem);
                _db.SaveChanges();
                return RedirectToAction("ShoppingCart");
            }
            ModelState.AddModelError("", "Invalid quantity (range 1-100)");
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveItem(CartItem item)
        {
            item = _db.DBCarts.Find(item.CartItemId);
            _db.DBCarts.Remove(item);
            _db.SaveChanges();
            return RedirectToAction("ShoppingCart", "Cart");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OrderPost()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login", "User");
            }
            string id = _userManager.GetUserId(User);
            float totalPrice = 0;
            IQueryable<CartItem> cartItems = _db.DBCarts.Where(obj => obj.Id == id && obj.OrderId == 1);
            foreach(var item in cartItems)
            {
                item.Component = _db.DBComponents.Find(item.ComponentId);
                item.PricePaid = item.Component.Price;
                totalPrice += item.PricePaid * item.Quantity;
            }
            Order order = new Order
            {
                Date = DateTime.Now,
                Price = totalPrice
            };
            _db.DBOrders.Add(order);
            _db.SaveChanges();
            order = _db.DBOrders.Where(obj => obj.Date == order.Date).FirstOrDefault();
            foreach (var item in cartItems)
            {
                item.OrderId = order.OrderId;
                _db.DBCarts.Update(item);
            }
            _db.SaveChanges();
            TempData["order id"] = order.OrderId;
            return RedirectToAction("Order");
        }
        public IActionResult Order(int ?id)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login", "User");
            }
            if (TempData["order id"] == null && (id == null || id == 0))
            {
                return NotFound();
            }
            if(TempData["order id"] != null)
            {
                id = (int)TempData["order id"];
            }
            IQueryable<CartItem> cartItems = _db.DBCarts.Where(obj => obj.OrderId == id);
            foreach (var item in cartItems)
            {
                item.Component = _db.DBComponents.Find(item.ComponentId);
            }
            return View(cartItems);
        }

        public IActionResult AllOrders()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login", "User");
            }
            string userId = _userManager.GetUserId(User);
            IQueryable<CartItem> cartItems = _db.DBCarts.Where(obj => obj.Id == userId && obj.OrderId != 1);
            IQueryable<int> queryable = (from item in cartItems select item.OrderId).Distinct();
            _db.SaveChanges();
            IQueryable<Order> orders = _db.DBOrders.Where(obj => queryable.Contains(obj.OrderId));
            return View(orders);
        }
    }
}
