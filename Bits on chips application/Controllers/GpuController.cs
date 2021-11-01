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
    public class GpuController : Controller
    {

        private readonly GpuService _gpuService;
        private readonly CategoryService _categoryService;
        private readonly WishItemService _wishItemService;
        public GpuController(GpuService gpuService, CategoryService categoryService, WishItemService wishItemService)
        {
            _gpuService = gpuService;
            _categoryService = categoryService;
            _wishItemService = wishItemService;
        }


        [HttpGet]
        [Route("Category/Gpu")]
        [Route("Store/Gpu")]
        public IActionResult Index()
        {
            IList<Gpu> objList = _gpuService.GetGpus();
            return View(objList);
        }

        [HttpGet]
        [Route("Gpu/Details")]
        public IActionResult Details(int id)
        {
            string userId = JwtMiddleware.getUserId(HttpContext);
            Gpu obj = _gpuService.GetGpuById(id);
            TempData["wish item"] = _wishItemService.GetWishItemsByCondition(x => x.UserId == userId && x.ComponentId == obj.ComponentId).FirstOrDefault();
            return View(obj);
        }

        //Get-Delete
        [Authorize]
        [HttpGet]
        [Route("Gpu/Delete")]
        public IActionResult Delete(int? id)
        {
            if (JwtMiddleware.IsUserInRole(HttpContext, Helper.Admin))
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var obj = _gpuService.GetGpuById(id);
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
        [Route("Gpu/DeletePost")]
        public IActionResult DeletePost(int GpuId)
        {
            var obj = _gpuService.GetGpuById(GpuId);
            if (obj == null)
            {
                return NotFound();
            }
            _gpuService.DeleteGpu(obj);
            _gpuService.Save();
            return RedirectToAction("Index");
        }

        //Get-Create
        [Authorize]
        [HttpGet]
        [Route("Gpu/Create")]
        public IActionResult Create()
        {
            if (JwtMiddleware.IsUserInRole(HttpContext, Helper.Admin))
            {
                Gpu GpuVM = new Gpu()
                {
                    Component = new Component()
                };
                return View(GpuVM);
            }
            return RedirectToAction("Index");
        }

        //Post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Gpu/Create")]
        public IActionResult Create(Gpu obj)
        {
            if (ModelState.IsValid)
            {
                obj.Component.CategoryId = _categoryService.GetCategoriesByCondition(x => x.CategoryName == "gpu").First().CategoryId;
                _gpuService.AddGpu(obj);
                _gpuService.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }

        //Get-Edit
        [Authorize]
        [HttpGet]
        [Route("Gpu/Edit")]
        public IActionResult Edit(int? id)
        {
            if (JwtMiddleware.IsUserInRole(HttpContext, Helper.Admin))
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var obj = _gpuService.GetGpuById(id);
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
        [Route("Gpu/Edit")]
        public IActionResult EditPost(Gpu item)
        {
            if (ModelState.IsValid)
            {
                _gpuService.UpdateGpu(item);
                _gpuService.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit", new { id = item.GpuId });
        }
    }
}
