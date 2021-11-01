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
    public class CpuController : Controller
    {

        private readonly CpuService _cpuService;
        private readonly CategoryService _categoryService;
        private readonly WishItemService _wishItemService;
        public CpuController(CpuService cpuService, CategoryService categoryService, WishItemService wishItemService)
        {
            _cpuService = cpuService;
            _categoryService = categoryService;
            _wishItemService = wishItemService;
        }


        [HttpGet]
        [Route("Category/Cpu")]
        [Route("Store/Cpu")]
        public IActionResult Index()
        {
            IList<Cpu> objList = _cpuService.GetCpus();
            return View(objList);
        }

        [HttpGet]
        [Route("Cpu/Details")]
        public IActionResult Details(int id)
        {
            string userId = JwtMiddleware.getUserId(HttpContext);
            Cpu obj = _cpuService.GetCpuById(id);
            TempData["wish item"] = _wishItemService.GetWishItemsByCondition(x => x.UserId == userId && x.ComponentId == obj.ComponentId).FirstOrDefault();
            return View(obj);
        }

        //Get-Delete
        [Authorize]
        [HttpGet]
        [Route("Cpu/Delete")]
        public IActionResult Delete(int? id)
        {
            if (JwtMiddleware.IsUserInRole(HttpContext, Helper.Admin))
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var obj = _cpuService.GetCpuById(id);
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
        [Route("Cpu/DeletePost")]
        public IActionResult DeletePost(int CpuId)
        {
            var obj = _cpuService.GetCpuById(CpuId);
            if (obj == null)
            {
                return NotFound();
            }
            _cpuService.DeleteCpu(obj);
            _cpuService.Save();
            return RedirectToAction("Index");
        }

        //Get-Create
        [Authorize]
        [HttpGet]
        [Route("Cpu/Create")]
        public IActionResult Create()
        {
            if (JwtMiddleware.IsUserInRole(HttpContext, Helper.Admin))
            {
                Cpu CpuVM = new Cpu()
                {
                    Component = new Component()
                };
                return View(CpuVM);
            }
            return RedirectToAction("Index");
        }

        //Post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Cpu/Create")]
        public IActionResult Create(Cpu obj)
        {
            if (ModelState.IsValid)
            {
                obj.Component.CategoryId = _categoryService.GetCategoriesByCondition(x => x.CategoryName == "cpu").First().CategoryId;
                _cpuService.AddCpu(obj);
                _cpuService.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }

        //Get-Edit
        [Authorize]
        [HttpGet]
        [Route("Cpu/Edit")]
        public IActionResult Edit(int? id)
        {
            if (JwtMiddleware.IsUserInRole(HttpContext, Helper.Admin))
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var obj = _cpuService.GetCpuById(id);
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
        [Route("Cpu/Edit")]
        public IActionResult EditPost(Cpu item)
        {
            if (ModelState.IsValid)
            {
                _cpuService.UpdateCpu(item);
                _cpuService.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit", new { id = item.CpuId });
        }
    }
}
