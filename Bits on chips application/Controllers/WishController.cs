using Bits_on_chips_application.Models;
using Bits_on_chips_application.Models.ViewModels;
using Bits_on_chips_application.Services;
using Bits_on_chips_application.Utility;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bits_on_chips_application.Controllers
{
    [Authorize]
    public class WishController : Controller
    {
        private readonly ComponentService _componentService;
        private readonly WishItemService _wishItemService;
        private readonly UserService _userService;
        public WishController(ComponentService componentService, WishItemService wishItemService, UserService userService)
        {
            _componentService = componentService;
            _wishItemService = wishItemService;
            _userService = userService;
        }

        [HttpGet]
        [Route("Wishlist")]
        [Route("Wishlist/Items")]
        public IActionResult Wishlist()
        {
            string userId = _userService.GetUserId(HttpContext);
            IList<WishItem> obj = _wishItemService.GetWishItemsByCondition(o => o.UserId == userId);
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Wishlist/AddItem")]
        public IActionResult AddItem(Component component)
        {
            string userId = _userService.GetUserId(HttpContext);
            component = _componentService.GetComponentById(component.ComponentId);
            WishItem wishItem = new WishItem
            {
                ComponentId = component.ComponentId,
                UserId = userId,
            };
            _wishItemService.AddWishItem(wishItem);
            _wishItemService.Save();
            TempData["item added"] = component.ComponentId.ToString();
            return RedirectToAction(component.Category.CategoryName, "Store");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Wishlist/RemoveItem")]
        public IActionResult RemoveItem(WishItem item)
        {
            item = _wishItemService.GetWishItemById(item.WishItemId);
            if (item == default(WishItem))
            {
                return NotFound();
            }
            _wishItemService.DeleteWishItem(item);
            _wishItemService.Save();
            return RedirectToAction("Wishlist", "Wish");
        }
    }
}
