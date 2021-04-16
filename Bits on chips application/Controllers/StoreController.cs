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

        public IActionResult Categories()
        {
            IEnumerable<Category> objList = _repoWrapper.Category.FindAll();
            return View(objList);
        }

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

        public IActionResult Ssd()
        {
            IQueryable<Component> objList = _repoWrapper.Component.FindByCondition(o => o.Category.CategoryName.Equals("ssd"));
            return View(objList);
        }

        public IActionResult Hdd()
        {
            IQueryable<Component> objList = _repoWrapper.Component.FindByCondition(o => o.Category.CategoryName.Equals("hdd"));
            return View(objList);
        }

        public IActionResult Ram()
        {
            IQueryable<Component> objList = _repoWrapper.Component.FindByCondition(o => o.Category.CategoryName.Equals("ram"));
            return View(objList);
        }

        public IActionResult Cpu()
        {
            IQueryable<Component> objList = _repoWrapper.Component.FindByCondition(o => o.Category.CategoryName.Equals("cpu"));
            return View(objList);
        }

        public IActionResult Gpu()
        {
            IQueryable<Component> objList = _repoWrapper.Component.FindByCondition(o => o.Category.CategoryName.Equals("gpu"));
            return View(objList);
        }

        public IActionResult Source()
        {
            IQueryable<Component> objList = _repoWrapper.Component.FindByCondition(o => o.Category.CategoryName.Equals("source"));
            return View(objList);
        }

        public IActionResult Cooler()
        {
            IQueryable<Component> objList = _repoWrapper.Component.FindByCondition(o => o.Category.CategoryName.Equals("cooler"));
            return View(objList);
        }

        public IActionResult Motherboard()
        {
            IQueryable<Component> objList = _repoWrapper.Component.FindByCondition(o => o.Category.CategoryName.Equals("motherboard"));
            return View(objList);
        }

        public IActionResult Case()
        {
            IQueryable<Component> objList = _repoWrapper.Component.FindByCondition(o => o.Category.CategoryName.Equals("case"));
            return View(objList);
        }

        //Get-Delete
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
