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
    public class SsdController : Controller
    {

        private readonly SsdService _ssdService;
        private readonly CategoryService _categoryService;
        private readonly WishItemService _wishItemService;
        public SsdController(SsdService ssdService, CategoryService categoryService, WishItemService wishItemService)
        {
            _ssdService = ssdService;
            _categoryService = categoryService;
            _wishItemService = wishItemService;
        }


        [HttpGet]
        [Route("Category/Ssd")]
        [Route("Store/Ssd")]
        public IActionResult Index()
        {
            IList<Ssd> objList = _ssdService.GetSsds();
            return View(objList);
        }

        [HttpGet]
        [Route("Ssd/Details")]
        public IActionResult Details(int id)
        {
            string userId = JwtMiddleware.getUserId(HttpContext);
            Ssd obj = _ssdService.GetSsdById(id);
            TempData["wish item"] = _wishItemService.GetWishItemsByCondition(x => x.UserId == userId && x.ComponentId == obj.ComponentId).FirstOrDefault();
            return View(obj);
        }

        //Get-Delete
        [Authorize]
        [HttpGet]
        [Route("Ssd/Delete")]
        public IActionResult Delete(int? id)
        {
            if (JwtMiddleware.IsUserInRole(HttpContext, Helper.Admin))
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var obj = _ssdService.GetSsdById(id);
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
        [Route("Ssd/DeletePost")]
        public IActionResult DeletePost(int SsdId)
        {
            var obj = _ssdService.GetSsdById(SsdId);
            if (obj == null)
            {
                return NotFound();
            }
            _ssdService.DeleteSsd(obj);
            _ssdService.Save();
            return RedirectToAction("Index");
        }

        //Get-Create
        [Authorize]
        [HttpGet]
        [Route("Ssd/Create")]
        public IActionResult Create()
        {
            if (JwtMiddleware.IsUserInRole(HttpContext, Helper.Admin))
            {
                Ssd SsdVM = new Ssd()
                {
                    Component = new Component()
                };
                return View(SsdVM);
            }
            return RedirectToAction("Index");
        }

        //Post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Ssd/Create")]
        public IActionResult Create(Ssd obj)
        {
            if (ModelState.IsValid)
            {
                obj.Component.CategoryId = _categoryService.GetCategoriesByCondition(x => x.CategoryName == "ssd").First().CategoryId;
                _ssdService.AddSsd(obj);
                _ssdService.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }

        //Get-Edit
        [Authorize]
        [HttpGet]
        [Route("Ssd/Edit")]
        public IActionResult Edit(int? id)
        {
            if (JwtMiddleware.IsUserInRole(HttpContext, Helper.Admin))
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var obj = _ssdService.GetSsdById(id);
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
        [Route("Ssd/Edit")]
        public IActionResult EditPost(Ssd item)
        {
            if (ModelState.IsValid)
            {
                _ssdService.UpdateSsd(item);
                _ssdService.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit", new { id = item.SsdId });
        }
    }
}
