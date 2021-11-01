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
    public class CoolerController : Controller
    {

        private readonly CoolerService _coolerService;
        private readonly CategoryService _categoryService;
        private readonly WishItemService _wishItemService;
        public CoolerController(CoolerService coolerService, CategoryService categoryService, WishItemService wishItemService)
        {
            _coolerService = coolerService;
            _categoryService = categoryService;
            _wishItemService = wishItemService;
        }


        [HttpGet]
        [Route("Category/Cooler")]
        [Route("Store/Cooler")]
        public IActionResult Index()
        {
            IList<Cooler> objList = _coolerService.GetCoolers();
            return View(objList);
        }

        [HttpGet]
        [Route("Cooler/Details")]
        public IActionResult Details(int id)
        {
            string userId = JwtMiddleware.getUserId(HttpContext);
            Cooler obj = _coolerService.GetCoolerById(id);
            TempData["wish item"] = _wishItemService.GetWishItemsByCondition(x => x.UserId == userId && x.ComponentId == obj.ComponentId).FirstOrDefault();
            return View(obj);
        }

        //Get-Delete
        [Authorize]
        [HttpGet]
        [Route("Cooler/Delete")]
        public IActionResult Delete(int? id)
        {
            if (JwtMiddleware.IsUserInRole(HttpContext, Helper.Admin))
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var obj = _coolerService.GetCoolerById(id);
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
        [Route("Cooler/DeletePost")]
        public IActionResult DeletePost(int CoolerId)
        {
            var obj = _coolerService.GetCoolerById(CoolerId);
            if (obj == null)
            {
                return NotFound();
            }
            _coolerService.DeleteCooler(obj);
            _coolerService.Save();
            return RedirectToAction("Index");
        }

        //Get-Create
        [Authorize]
        [HttpGet]
        [Route("Cooler/Create")]
        public IActionResult Create()
        {
            if (JwtMiddleware.IsUserInRole(HttpContext, Helper.Admin))
            {
                Cooler CoolerVM = new Cooler()
                {
                    Component = new Component()
                };
                return View(CoolerVM);
            }
            return RedirectToAction("Index");
        }

        //Post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Cooler/Create")]
        public IActionResult Create(Cooler obj)
        {
            if (ModelState.IsValid)
            {
                obj.Component.CategoryId = _categoryService.GetCategoriesByCondition(x => x.CategoryName == "cooler").First().CategoryId;
                _coolerService.AddCooler(obj);
                _coolerService.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }

        //Get-Edit
        [Authorize]
        [HttpGet]
        [Route("Cooler/Edit")]
        public IActionResult Edit(int? id)
        {
            if (JwtMiddleware.IsUserInRole(HttpContext, Helper.Admin))
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var obj = _coolerService.GetCoolerById(id);
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
        [Route("Cooler/Edit")]
        public IActionResult EditPost(Cooler item)
        {
            if (ModelState.IsValid)
            {
                _coolerService.UpdateCooler(item);
                _coolerService.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit", new { id = item.CoolerId });
        }
    }
}
