using Bits_on_chips_application.Data;
using Bits_on_chips_application.Models;
using Bits_on_chips_application.Models.ViewModels;
using Bits_on_chips_application.Services;
using Bits_on_chips_application.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bits_on_chips_application.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ComponentService _componentService;
        private readonly CartItemService _cartItemService;
        private readonly OrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;
        public CartController(ComponentService componentService, CartItemService cartItemService, OrderService orderService, UserManager<ApplicationUser> userManager)
        {
            _componentService = componentService;
            _cartItemService = cartItemService;
            _orderService = orderService;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("ShoppingCart")]
        [Route("ShoppingCart/Items")]
        public IActionResult ShoppingCart()
        {
            string userId = _userManager.GetUserId(User);
            IList<CartItem> obj = _cartItemService.GetCartItemsByCondition(o => o.Id == userId && o.OrderId == 1);
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ShoppingCart/AddItem")]
        public IActionResult AddItem(Component component)
        {
            string userId = _userManager.GetUserId(User);
            component = _componentService.GetComponentById(component.ComponentId);
            CartItem cartItem = new CartItem
            {
                ComponentId = component.ComponentId,
                Id = userId,
                OrderId = 1,
                Quantity = 1
            };
            _cartItemService.AddCartItem(cartItem);
            _cartItemService.Save();
            TempData["item added"] = component.ComponentId.ToString();
            return RedirectToAction(component.Category.CategoryName, "Store");
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
            CartItem item = _cartItemService.GetCartItemById(id);
            if (item == null)
            {
                return NotFound();
            }
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
                CartItem cartItem = _cartItemService.GetCartItemById(item.CartItemId);
                if (cartItem == null)
                {
                    return NotFound();
                }
                cartItem.Quantity = item.Quantity;
                _cartItemService.UpdateCartItem(cartItem);
                _cartItemService.Save();
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
            item = _cartItemService.GetCartItemById(item.CartItemId);
            if(item == default(CartItem))
            {
                return NotFound();
            }
            _cartItemService.DeleteCartItem(item);
            _cartItemService.Save();
            return RedirectToAction("ShoppingCart", "Cart");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ShoppingCart/Order")]
        public IActionResult OrderPost()
        {
            string id = _userManager.GetUserId(User);
            float totalPrice = 0;
            IList<CartItem> cartItems = _cartItemService.GetCartItemsByCondition(obj => obj.Id == id && obj.OrderId == 1).ToList();
            foreach(var item in cartItems)
            {
                item.PricePaid = item.Component.Price;
                totalPrice += item.Component.Price * item.Quantity;
            }
            Order order = new Order
            {
                Date = DateTime.Now,
                Price = totalPrice
            };
            _orderService.AddOrder(order);
            _orderService.Save();
            order = _orderService.GetOrdersByCondition(obj => obj.Date == order.Date && order.Price == totalPrice).FirstOrDefault();
            _cartItemService.UpdateCartItemsForOrder(cartItems, order);
            _cartItemService.Save();
            TempData["order id"] = order.OrderId;
            return RedirectToAction("Order");
        }

        [HttpGet]
        [Route("Order")]
        public IActionResult Order(int ?id)
        {
            if (TempData["order id"] == null && (id == null || id == 0))
            {
                return NotFound();
            }
            if(TempData["order id"] != null)
            {
                id = (int)TempData["order id"];
            }
            IList<CartItem> cartItems = _cartItemService.GetCartItemsByCondition(obj => obj.OrderId == id).ToList();
            return View(cartItems);
        }

        [HttpGet]
        [Route("AllOrders")]
        public IActionResult AllOrders()
        {
            string userId = _userManager.GetUserId(User);
            List<Order> orders = _orderService.GetOrdersForUser(userId);
            return View(orders);
        }
    }
}
