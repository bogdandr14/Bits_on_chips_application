using Bits_on_chips_application.Models;
using Bits_on_chips_application.Models.ViewModels;
using Bits_on_chips_application.Services;
using Bits_on_chips_application.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Bits_on_chips_application.Controllers
{
    public class StoreController : Controller
    {
        private readonly ComponentService _componentService;
        private readonly CategoryService _categoryService;
        public StoreController(ComponentService componentService, CategoryService categoryService)
        {
            _componentService = componentService;
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("Store")]
        [Route("Store/Categories")]
        public IActionResult Categories()
        {
            IEnumerable<Category> objList = _categoryService.GetCategories();
            return View(objList);
        }

        [HttpGet]
        [Route("Store/Category")]
        public IActionResult Category(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound(id);
            }
            Category category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound(id);
            }
            return RedirectToAction(category.CategoryName);
        }

        [HttpGet]
        [Route("Category/Ssd")]
        [Route("Store/Ssd")]
        public IActionResult Ssd()
        {
            IList<Component> objList = _componentService.GetComponentsByCondition(o => o.Category.CategoryName.Equals("ssd"));
            return View(objList);
        }

        [HttpGet]
        [Route("Category/Hdd")]
        [Route("Store/Hdd")]
        public IActionResult Hdd()
        {
            IList<Component> objList = _componentService.GetComponentsByCondition(o => o.Category.CategoryName.Equals("hdd"));
            return View(objList);
        }

        [HttpGet]
        [Route("Category/Ram")]
        [Route("Store/Ram")]
        public IActionResult Ram()
        {
            IList<Component> objList = _componentService.GetComponentsByCondition(o => o.Category.CategoryName.Equals("ram"));
            return View(objList);
        }

        [HttpGet]
        [Route("Category/Cpu")]
        [Route("Store/Cpu")]
        public IActionResult Cpu()
        {
            IList<Component> objList = _componentService.GetComponentsByCondition(o => o.Category.CategoryName.Equals("cpu"));
            return View(objList);
        }

        [HttpGet]
        [Route("Category/Gpu")]
        [Route("Store/Gpu")]
        public IActionResult Gpu()
        {
            IList<Component> objList = _componentService.GetComponentsByCondition(o => o.Category.CategoryName.Equals("gpu"));
            return View(objList);
        }

        [HttpGet]
        [Route("Category/Source")]
        [Route("Store/Source")]
        public IActionResult Source()
        {
            IList<Component> objList = _componentService.GetComponentsByCondition(o => o.Category.CategoryName.Equals("source"));
            return View(objList);
        }

        [HttpGet]
        [Route("Category/Cooler")]
        [Route("Store/Cooler")]
        public IActionResult Cooler()
        {
            IList<Component> objList = _componentService.GetComponentsByCondition(o => o.Category.CategoryName.Equals("cooler"));
            return View(objList);
        }

        [HttpGet]
        [Route("Category/Motherboard")]
        [Route("Store/Motherboard")]
        public IActionResult Motherboard()
        {
            IList<Component> objList = _componentService.GetComponentsByCondition(o => o.Category.CategoryName.Equals("motherboard"));
            return View(objList);
        }

        [HttpGet]
        [Route("Category/Case")]
        [Route("Store/Case")]
        public IActionResult Case()
        {
            IList<Component> objList = _componentService.GetComponentsByCondition(o => o.Category.CategoryName.Equals("case"));
            return View(objList);
        }

        //Get-Delete
        [Authorize(Roles = Helper.Admin)]
        [HttpGet]
        [Route("Component/Delete")]
        public IActionResult Delete(int? id)
        {
            if (Authentication.Jwt.JwtMiddleware.IsUserInRole(HttpContext, Helper.Admin)){
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var obj = _componentService.GetComponentById(id);
                if (obj == null)
                {
                    return NotFound();
                }
                return View(obj);
            }
            return RedirectToAction("Categories");
        }

        //Post-Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Component/DeletePost")]
        public IActionResult DeletePost(int ComponentId)
        {
            var obj = _componentService.GetComponentById(ComponentId);
            if (obj == null)
            {
                return NotFound();
            }
            _componentService.DeleteComponent(obj);
            _componentService.Save();
            return RedirectToAction("Categories");
        }

        //Get-Create
        [Authorize]
        [HttpGet]
        [Route("Component/Create")]
        public IActionResult Create()
        {
            if (Authentication.Jwt.JwtMiddleware.IsUserInRole(HttpContext, Helper.Admin)){
                ComponentVM ComponentVM = new ComponentVM()
                {
                    Component = new Component(),
                    TypeDropDown = _categoryService.GetCategories().Select(i => new SelectListItem
                    {
                        Text = i.CategoryName,
                        Value = i.CategoryId.ToString()
                    })
                };
                return View(ComponentVM);
            }
            return RedirectToAction("Categories");
        }

        //Post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Component/Create")]
        public IActionResult Create(ComponentVM obj)
        {
            if (ModelState.IsValid)
            {
                _componentService.AddComponent(obj.Component);
                _componentService.Save();
                return RedirectToAction("Categories");
            }
            IEnumerable<SelectListItem> TypeDropDown = _categoryService.GetCategories().Select(i => new SelectListItem
            {
                Text = i.CategoryName,
                Value = i.CategoryId.ToString()
            });
            ViewBag.TypeDropDown = TypeDropDown;
            return View(obj);
        }

        //Get-Edit
        [Authorize]
        [HttpGet]
        [Route("Component/Edit")]
        public IActionResult Edit(int? id)
        {
            if (Authentication.Jwt.JwtMiddleware.IsUserInRole(HttpContext, Helper.Admin)){
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var obj = _componentService.GetComponentById(id);
                if (obj == null)
                {
                    return NotFound();
                }
                return View(obj);
            }
            return RedirectToAction("Categories");
        }

        //Post-Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Component/Edit")]
        public IActionResult EditPost(Component component)
        {
            if (ModelState.IsValid)
            {
                _componentService.UpdateComponent(component);
                _componentService.Save();
                return RedirectToAction("Categories");
            }
            return RedirectToAction("Edit", new { id = component.ComponentId });
        }
    }
}
