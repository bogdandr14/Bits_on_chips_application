using Bits_on_chips_application.Data;
using Bits_on_chips_application.Models;
using Bits_on_chips_application.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vereyon.Web;

namespace Bits_on_chips_application.Controllers
{
    public class StoreController : Controller
    {
        private readonly IRepositoryWrapper _repoWrapper;
        public StoreController(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        [HttpGet]
        [Route("Store")]
        [Route("Store/Categories")]
        public IActionResult Categories()
        {
            IEnumerable<Category> objList = _repoWrapper.Category.FindAll();
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
            Category category = _repoWrapper.Category.FindById(id);
            if (category == null)
            {
                return NotFound(id);
            }
            return RedirectToAction(category.CategoryName);
        }

        [HttpGet]
        [Route("Category/Rsd")]
        [Route("Store/Rsd")]
        public IActionResult Ssd()
        {
            IQueryable<Component> objList = _repoWrapper.Component.FindByCondition(o => o.Category.CategoryName.Equals("ssd"));
            return View(objList);
        }

        [HttpGet]
        [Route("Category/Hdd")]
        [Route("Store/Hdd")]
        public IActionResult Hdd()
        {
            IQueryable<Component> objList = _repoWrapper.Component.FindByCondition(o => o.Category.CategoryName.Equals("hdd"));
            return View(objList);
        }

        [HttpGet]
        [Route("Category/Ram")]
        [Route("Store/Ram")]
        public IActionResult Ram()
        {
            IQueryable<Component> objList = _repoWrapper.Component.FindByCondition(o => o.Category.CategoryName.Equals("ram"));
            return View(objList);
        }

        [HttpGet]
        [Route("Category/Cpu")]
        [Route("Store/Cpu")]
        public IActionResult Cpu()
        {
            IQueryable<Component> objList = _repoWrapper.Component.FindByCondition(o => o.Category.CategoryName.Equals("cpu"));
            return View(objList);
        }

        [HttpGet]
        [Route("Category/Gpu")]
        [Route("Store/Gpu")]
        public IActionResult Gpu()
        {
            IQueryable<Component> objList = _repoWrapper.Component.FindByCondition(o => o.Category.CategoryName.Equals("gpu"));
            return View(objList);
        }

        [HttpGet]
        [Route("Category/Source")]
        [Route("Store/Source")]
        public IActionResult Source()
        {
            IQueryable<Component> objList = _repoWrapper.Component.FindByCondition(o => o.Category.CategoryName.Equals("source"));
            return View(objList);
        }

        [HttpGet]
        [Route("Category/Cooler")]
        [Route("Store/Cooler")]
        public IActionResult Cooler()
        {
            IQueryable<Component> objList = _repoWrapper.Component.FindByCondition(o => o.Category.CategoryName.Equals("cooler"));
            return View(objList);
        }

        [HttpGet]
        [Route("Category/Motherboard")]
        [Route("Store/Motherboard")]
        public IActionResult Motherboard()
        {
            IQueryable<Component> objList = _repoWrapper.Component.FindByCondition(o => o.Category.CategoryName.Equals("motherboard"));
            return View(objList);
        }

        [HttpGet]
        [Route("Category/Case")]
        [Route("Store/Case")]
        public IActionResult Case()
        {
            IQueryable<Component> objList = _repoWrapper.Component.FindByCondition(o => o.Category.CategoryName.Equals("case"));
            return View(objList);
        }

        //Get-Delete
        [HttpGet]
        [Route("Component/Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _repoWrapper.Component.FindById(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //Post-Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Component/DeletePost")]
        public IActionResult DeletePost(int ComponentId)
        {
            var obj = _repoWrapper.Component.FindById(ComponentId);
            if (obj == null)
            {
                return NotFound();
            }
            _repoWrapper.Component.Delete(obj);
            _repoWrapper.Save();
            return RedirectToAction("Categories");
        }

        //Get-Create
        [HttpGet]
        [Route("Component/Create")]
        public IActionResult Create()
        {
            ComponentVM ComponentVM = new ComponentVM()
            {
                Component = new Component(),
                TypeDropDown = _repoWrapper.Category.FindAll().Select(i => new SelectListItem
                {
                    Text = i.CategoryName,
                    Value = i.CategoryId.ToString()
                })
            };
            return View(ComponentVM);
        }

        //Post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Component/Create")]
        public IActionResult Create(ComponentVM obj)
        {
            if (ModelState.IsValid)
            {
                _repoWrapper.Component.Create(obj.Component);
                _repoWrapper.Save();
                return RedirectToAction("Categories");
            }
            IEnumerable<SelectListItem> TypeDropDown = _repoWrapper.Category.FindAll().Select(i => new SelectListItem
            {
                Text = i.CategoryName,
                Value = i.CategoryId.ToString()
            });
            ViewBag.TypeDropDown = TypeDropDown;
            return View(obj);
        }

        //Get-Edit
        [HttpGet]
        [Route("Component/Edit")]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _repoWrapper.Component.FindById(id);
            if (obj == null)
            {
                return NotFound();
            }
            var categ = _repoWrapper.Category.FindById(obj.CategoryId);
            if (categ == null)
            {
                return NotFound();
            }
            obj.Category = categ;
            return View(obj);
        }

        //Post-Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Component/Edit")]
        public IActionResult EditPost(Component component)
        {
            if (ModelState.IsValid)
            {
                _repoWrapper.Component.Update(component);
                _repoWrapper.Save();
                return RedirectToAction("Categories");
            }
            return RedirectToAction("Edit", new { id = component.ComponentId });
        }
    }
}
