using Authentication.Jwt;
using Bits_on_chips_application.Models;
using Bits_on_chips_application.Services;
using Bits_on_chips_application.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Bits_on_chips_application.Controllers
{
    public class RamController : Controller
    {

        private readonly RamService _ramService;
        private readonly CategoryService _categoryService;
        private readonly WishItemService _wishItemService;
        public RamController(RamService ramService, CategoryService categoryService, WishItemService wishItemService)
        {
            _ramService = ramService;
            _categoryService = categoryService;
            _wishItemService = wishItemService;
        }


        [HttpGet]
        [Route("Category/Ram")]
        [Route("Store/Ram")]
        public IActionResult Index()
        {
            IList<Ram> objList = _ramService.GetRams();
            return View(objList);
        }

        [HttpGet]
        [Route("Ram/Details")]
        public IActionResult Details(int id)
        {
            string userId = JwtMiddleware.getUserId(HttpContext);
            Ram obj = _ramService.GetRamById(id);
            TempData["wish item"] = _wishItemService.GetWishItemsByCondition(x => x.UserId == userId && x.ComponentId == obj.ComponentId).FirstOrDefault();
            return View(obj);
        }

        //Get-Delete
        [Authorize]
        [HttpGet]
        [Route("Ram/Delete")]
        public IActionResult Delete(int? id)
        {
            if (JwtMiddleware.IsUserInRole(HttpContext, Helper.Admin))
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var obj = _ramService.GetRamById(id);
                if (obj == null)
                {
                    return NotFound();
                }
                return View(obj);
            }
            return RedirectToAction("Index");
        }

        //Post-Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Ram/DeletePost")]
        public IActionResult DeletePost(int RamId)
        {
            var obj = _ramService.GetRamById(RamId);
            if (obj == null)
            {
                return NotFound();
            }
            _ramService.DeleteRam(obj);
            _ramService.Save();
            return RedirectToAction("Index");
        }

        //Get-Create
        [Authorize]
        [HttpGet]
        [Route("Ram/Create")]
        public IActionResult Create()
        {
            if (JwtMiddleware.IsUserInRole(HttpContext, Helper.Admin))
            {
                Ram RamVM = new Ram()
                {
                    Component = new Component()
                };
                return View(RamVM);
            }
            return RedirectToAction("Index");
        }

        //Post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Ram/Create")]
        public IActionResult Create(Ram obj)
        {
            if (ModelState.IsValid)
            {
                obj.Component.CategoryId = _categoryService.GetCategoriesByCondition(x => x.CategoryName == "ram").First().CategoryId;
                _ramService.AddRam(obj);
                _ramService.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }

        //Get-Edit
        [Authorize]
        [HttpGet]
        [Route("Ram/Edit")]
        public IActionResult Edit(int? id)
        {
            if (JwtMiddleware.IsUserInRole(HttpContext, Helper.Admin))
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var obj = _ramService.GetRamById(id);
                if (obj == null)
                {
                    return NotFound();
                }
                return View(obj);
            }
            return RedirectToAction("Index");
        }

        //Post-Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Ram/Edit")]
        public IActionResult EditPost(Ram item)
        {
            if (ModelState.IsValid)
            {
                _ramService.UpdateRam(item);
                _ramService.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit", new { id = item.RamId });
        }
    }
}
