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
        private readonly IRepositoryWrapper _repoWrapper;
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        public CartController(IRepositoryWrapper repoWrapper, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _repoWrapper = repoWrapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [Route("ShoppingCart")]
        [Route("ShoppingCart/Items")]
        public IActionResult ShoppingCart()
        {
            string userId = _userManager.GetUserId(User);
            IQueryable<CartItem> obj = _repoWrapper.CartItem.FindByCondition(o => o.Id == userId && o.OrderId == 1);
            IList<CartItem> result = new List<CartItem>();
            foreach(var item in obj)
            {
                item.Component = _repoWrapper.Component.FindById(item.ComponentId);
                item.Component.Category = _repoWrapper.Category.FindById(item.Component.CategoryId);
                result.Add(item);
            }
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ShoppingCart/AddItem")]
        public IActionResult AddItem(Component component)
        {
            string userId = _userManager.GetUserId(User);
            component = _repoWrapper.Component.FindById(component.ComponentId);
            CartItem cartItem = _repoWrapper.CartItem.FindByCondition(obj => obj.ComponentId == component.ComponentId && obj.Id == userId && obj.OrderId == 1).FirstOrDefault();
            if (cartItem == default(CartItem))
            {
                cartItem = new CartItem
                {
                    ComponentId = component.ComponentId,
                    Id = userId,
                    OrderId = 1,
                    Quantity = 1
                };
                _repoWrapper.CartItem.Create(cartItem);
            }
            else
            {
                ++cartItem.Quantity;
                _repoWrapper.CartItem.Update(cartItem);
            }
            _repoWrapper.Save();
            Category category = _repoWrapper.Category.FindById(component.CategoryId);
            TempData["item added"] = component.ComponentId.ToString();
            return RedirectToAction(category.CategoryName, "Store");
        }

        [HttpGet]
        [Route("ShoppingCart/ChangeQuantity")]
        [Route("CartItem/ChangeQuantity")]
        public IActionResult ChangeQuantity(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            CartItem item = _repoWrapper.CartItem.FindById(id);
            if (item == null)
            {
                return NotFound();
            }
            item.Component = _repoWrapper.Component.FindById(item.ComponentId);
            item.Component.Category = _repoWrapper.Category.FindById(item.Component.CategoryId);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ShoppingCart/ChangeQuantity")]
        [Route("CartItem/ChangeQuantity")]
        public IActionResult ChangeQuantity(CartItem item)
        {
            if (ModelState.IsValid)
            {
                CartItem cartItem = _repoWrapper.CartItem.FindById(item.CartItemId);
                if (cartItem == null)
                {
                    return NotFound();
                }
                cartItem.Quantity = item.Quantity;
                _repoWrapper.CartItem.Update(cartItem);
                _repoWrapper.Save();
                return RedirectToAction("ShoppingCart");
            }
            ModelState.AddModelError("", "Invalid quantity (range 1-100)");
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ShoppingCart/RemoveItem")]
        public IActionResult RemoveItem(CartItem item)
        {
            item = _repoWrapper.CartItem.FindById(item.CartItemId);
            if(item == default(CartItem))
            {
                return NotFound();
            }
            _repoWrapper.CartItem.Delete(item);
            _repoWrapper.Save();
            return RedirectToAction("ShoppingCart", "Cart");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ShoppingCart/Order")]
        public IActionResult OrderPost()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login", "User");
            }
            string id = _userManager.GetUserId(User);
            float totalPrice = 0;
            IList<CartItem> cartItems = _repoWrapper.CartItem.FindByCondition(obj => obj.Id == id && obj.OrderId == 1).ToList();
            foreach(var item in cartItems)
            {
                item.Component = _repoWrapper.Component.FindById(item.ComponentId);
                item.PricePaid = item.Component.Price;
                totalPrice += item.Component.Price * item.Quantity;
            }
            Order order = new Order
            {
                Date = DateTime.Now,
                Price = totalPrice
            };
            _repoWrapper.Order.Create(order);
            _repoWrapper.Save();
            order = _repoWrapper.Order.FindByCondition(obj => obj.Date == order.Date).FirstOrDefault();
            foreach (var item in cartItems)
            {
                item.OrderId = order.OrderId;
                _repoWrapper.CartItem.Update(item);
            }
            _repoWrapper.Save();
            TempData["order id"] = order.OrderId;
            return RedirectToAction("Order");
        }

        [HttpGet]
        [Route("Order")]
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
            IQueryable<CartItem> cartItems = _repoWrapper.CartItem.FindByCondition(obj => obj.OrderId == id);
            IList<CartItem> result = new List<CartItem>();
            foreach (var item in cartItems)
            {
                item.Component = _repoWrapper.Component.FindById(item.ComponentId);
                result.Add(item);
            }
            return View(result);
        }

        [HttpGet]
        [Route("AllOrders")]
        public IActionResult AllOrders()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login", "User");
            }
            string userId = _userManager.GetUserId(User);
            IQueryable<CartItem> cartItems = _repoWrapper.CartItem.FindByCondition(obj => obj.Id == userId && obj.OrderId != 1);
            IQueryable<int> queryable = (from item in cartItems select item.OrderId).Distinct();
            IQueryable<Order> orders = _repoWrapper.Order.FindByCondition(obj => queryable.Contains(obj.OrderId));
            return View(orders);
        }
    }
}
