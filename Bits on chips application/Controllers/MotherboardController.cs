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
    public class MotherboardController : Controller
    {

        private readonly MotherboardService _motherboardService;
        private readonly CategoryService _categoryService;
        private readonly WishItemService _wishItemService;
        public MotherboardController(MotherboardService motherboardService, CategoryService categoryService, WishItemService wishItemService)
        {
            _motherboardService = motherboardService;
            _categoryService = categoryService;
            _wishItemService = wishItemService;
        }


        [HttpGet]
        [Route("Category/Motherboard")]
        [Route("Store/Motherboard")]
        public IActionResult Index()
        {
            IList<Motherboard> objList = _motherboardService.GetMotherboards();
            return View(objList);
        }

        [HttpGet]
        [Route("Motherboard/Details")]
        public IActionResult Details(int id)
        {
            string userId = JwtMiddleware.getUserId(HttpContext);
            Motherboard obj = _motherboardService.GetMotherboardById(id);
            TempData["wish item"] = _wishItemService.GetWishItemsByCondition(x => x.UserId == userId && x.ComponentId == obj.ComponentId).FirstOrDefault();
            return View(obj);
        }

        //Get-Delete
        [Authorize]
        [HttpGet]
        [Route("Motherboard/Delete")]
        public IActionResult Delete(int? id)
        {
            if (JwtMiddleware.IsUserInRole(HttpContext, Helper.Admin))
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var obj = _motherboardService.GetMotherboardById(id);
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
        [Route("Motherboard/DeletePost")]
        public IActionResult DeletePost(int MotherboardId)
        {
            var obj = _motherboardService.GetMotherboardById(MotherboardId);
            if (obj == null)
            {
                return NotFound();
            }
            _motherboardService.DeleteMotherboard(obj);
            _motherboardService.Save();
            return RedirectToAction("Index");
        }

        //Get-Create
        [Authorize]
        [HttpGet]
        [Route("Motherboard/Create")]
        public IActionResult Create()
        {
            if (JwtMiddleware.IsUserInRole(HttpContext, Helper.Admin))
            {
                Motherboard MotherboardVM = new Motherboard()
                {
                    Component = new Component()
                };
                return View(MotherboardVM);
            }
            return RedirectToAction("Index");
        }

        //Post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Motherboard/Create")]
        public IActionResult Create(Motherboard obj)
        {
            if (ModelState.IsValid)
            {
                obj.Component.CategoryId = _categoryService.GetCategoriesByCondition(x => x.CategoryName == "motherboard").First().CategoryId;
                _motherboardService.AddMotherboard(obj);
                _motherboardService.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }

        //Get-Edit
        [Authorize]
        [HttpGet]
        [Route("Motherboard/Edit")]
        public IActionResult Edit(int? id)
        {
            if (JwtMiddleware.IsUserInRole(HttpContext, Helper.Admin))
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var obj = _motherboardService.GetMotherboardById(id);
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
        [Route("Motherboard/Edit")]
        public IActionResult EditPost(Motherboard item)
        {
            if (ModelState.IsValid)
            {
                _motherboardService.UpdateMotherboard(item);
                _motherboardService.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit", new { id = item.MotherboardId });
        }
    }
}
