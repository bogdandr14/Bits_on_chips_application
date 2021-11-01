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
    public class SourceController : Controller
    {

        private readonly SourceService _sourceService;
        private readonly CategoryService _categoryService;
        private readonly WishItemService _wishItemService;
        public SourceController(SourceService sourceService, CategoryService categoryService, WishItemService wishItemService)
        {
            _sourceService = sourceService;
            _categoryService = categoryService;
            _wishItemService = wishItemService;
        }


        [HttpGet]
        [Route("Category/Source")]
        [Route("Store/Source")]
        public IActionResult Index()
        {
            IList<Source> objList = _sourceService.GetSources();
            return View(objList);
        }

        [HttpGet]
        [Route("Source/Details")]
        public IActionResult Details(int id)
        {
            string userId = JwtMiddleware.getUserId(HttpContext);
            Source obj = _sourceService.GetSourceById(id);
            TempData["wish item"] = _wishItemService.GetWishItemsByCondition(x => x.UserId == userId && x.ComponentId == obj.ComponentId).FirstOrDefault();
            return View(obj);
        }

        //Get-Delete
        [Authorize]
        [HttpGet]
        [Route("Source/Delete")]
        public IActionResult Delete(int? id)
        {
            if (JwtMiddleware.IsUserInRole(HttpContext, Helper.Admin))
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var obj = _sourceService.GetSourceById(id);
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
        [Route("Source/DeletePost")]
        public IActionResult DeletePost(int SourceId)
        {
            var obj = _sourceService.GetSourceById(SourceId);
            if (obj == null)
            {
                return NotFound();
            }
            _sourceService.DeleteSource(obj);
            _sourceService.Save();
            return RedirectToAction("Index");
        }

        //Get-Create
        [Authorize]
        [HttpGet]
        [Route("Source/Create")]
        public IActionResult Create()
        {
            if (JwtMiddleware.IsUserInRole(HttpContext, Helper.Admin))
            {
                Source SourceVM = new Source()
                {
                    Component = new Component()
                };
                return View(SourceVM);
            }
            return RedirectToAction("Index");
        }

        //Post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Source/Create")]
        public IActionResult Create(Source obj)
        {
            if (ModelState.IsValid)
            {
                obj.Component.CategoryId = _categoryService.GetCategoriesByCondition(x => x.CategoryName == "source").First().CategoryId;
                _sourceService.AddSource(obj);
                _sourceService.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }

        //Get-Edit
        [Authorize]
        [HttpGet]
        [Route("Source/Edit")]
        public IActionResult Edit(int? id)
        {
            if (JwtMiddleware.IsUserInRole(HttpContext, Helper.Admin))
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var obj = _sourceService.GetSourceById(id);
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
        [Route("Source/Edit")]
        public IActionResult EditPost(Source item)
        {
            if (ModelState.IsValid)
            {
                _sourceService.UpdateSource(item);
                _sourceService.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit", new { id = item.SourceId });
        }
    }
}
