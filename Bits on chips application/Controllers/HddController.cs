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
    public class HddController : Controller
    {

        private readonly HddService _hddService;
        private readonly CategoryService _categoryService;
        private readonly WishItemService _wishItemService;
        public HddController(HddService hddService, CategoryService categoryService, WishItemService wishItemService)
        {
            _hddService = hddService;
            _categoryService = categoryService;
            _wishItemService = wishItemService;
        }


        [HttpGet]
        [Route("Category/Hdd")]
        [Route("Store/Hdd")]
        public IActionResult Index()
        {
            IList<Hdd> objList = _hddService.GetHdds();
            return View(objList);
        }

        [HttpGet]
        [Route("Hdd/Details")]
        public IActionResult Details(int id)
        {
            string userId = JwtMiddleware.getUserId(HttpContext);
            Hdd obj = _hddService.GetHddById(id);
            TempData["wish item"] = _wishItemService.GetWishItemsByCondition(x => x.UserId == userId && x.ComponentId == obj.ComponentId).FirstOrDefault();
            return View(obj);
        }

        //Get-Delete
        [Authorize]
        [HttpGet]
        [Route("Hdd/Delete")]
        public IActionResult Delete(int? id)
        {
            if (JwtMiddleware.IsUserInRole(HttpContext, Helper.Admin))
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var obj = _hddService.GetHddById(id);
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
        [Route("Hdd/DeletePost")]
        public IActionResult DeletePost(int HddId)
        {
            var obj = _hddService.GetHddById(HddId);
            if (obj == null)
            {
                return NotFound();
            }
            _hddService.DeleteHdd(obj);
            _hddService.Save();
            return RedirectToAction("Index");
        }

        //Get-Create
        [Authorize]
        [HttpGet]
        [Route("Hdd/Create")]
        public IActionResult Create()
        {
            if (JwtMiddleware.IsUserInRole(HttpContext, Helper.Admin))
            {
                Hdd HddVM = new Hdd()
                {
                    Component = new Component()
                };
                return View(HddVM);
            }
            return RedirectToAction("Index");
        }

        //Post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Hdd/Create")]
        public IActionResult Create(Hdd obj)
        {
            if (ModelState.IsValid)
            {
                obj.Component.CategoryId = _categoryService.GetCategoriesByCondition(x => x.CategoryName == "hdd").First().CategoryId;
                _hddService.AddHdd(obj);
                _hddService.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }

        //Get-Edit
        [Authorize]
        [HttpGet]
        [Route("Hdd/Edit")]
        public IActionResult Edit(int? id)
        {
            if (JwtMiddleware.IsUserInRole(HttpContext, Helper.Admin))
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var obj = _hddService.GetHddById(id);
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
        [Route("Hdd/Edit")]
        public IActionResult EditPost(Hdd item)
        {
            if (ModelState.IsValid)
            {
                _hddService.UpdateHdd(item);
                _hddService.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit", new { id = item.HddId });
        }
    }
}
